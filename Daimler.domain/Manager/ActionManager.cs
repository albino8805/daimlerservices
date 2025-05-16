using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using ActionEntity = Daimler.data.Models.Action;

namespace Daimler.domain.Manager
{
    public class ActionManager : BaseManager<ActionViewModel, ActionEntity>, IActionManager
    {
        private readonly IActionRepository _actionRepository;

        public ActionManager(IFolderHelper folderHelper, IActionRepository respository) : base(folderHelper, respository)
        {
            _actionRepository = respository;
        }

        public override ActionViewModel Add(ActionViewModel viewModel)
        {
            if (_actionRepository.ValidateName(viewModel.Name))
            {
                throw new Exception($"El registro {viewModel.Name} ya se encuentra registrado.");
            }

            ActionEntity entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override void Update(int id, ActionViewModel viewModel)
        {
            if (_actionRepository.ValidateName(viewModel.Name))
            {
                throw new Exception("El registro " + viewModel.Name + " ya se encuentra registrado.");
            }

            ActionEntity entity = _repository.GetById(id);

            if (entity == null)
            {
                throw new Exception("No se encontró el registro.");
            }

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public override ActionEntity AddConverter(ActionViewModel viewModel)
        {
            return new ActionEntity()
            {
                Name = viewModel.Name,
                Active = Constants.True
            };
        }

        public override List<ActionViewModel> CollectionConverter(List<ActionEntity> entities)
        {
            return entities.Select(e => new ActionViewModel
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        public override ActionEntity UpdatedConverter(ActionViewModel viewModel, ActionEntity entity)
        {
            entity.Name = viewModel.Name;

            return entity;
        }

        public override ActionViewModel SingleConverter(ActionEntity entity)
        {
            return new ActionViewModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
