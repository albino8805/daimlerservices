using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;

namespace Daimler.domain.Manager
{
    public class StateManager : BaseManager<StateViewModel, State>, IStateManager
    {
        private readonly IStateRepository _stateRepository;

        public StateManager(IFolderHelper folderHelper, IStateRepository repository) : base(folderHelper, repository)
        {
            _stateRepository = repository;
        }

        public override StateViewModel Add(StateViewModel viewModel)
        {
            if (_stateRepository.ValidateNameAndCountry(viewModel.Name, viewModel.CountryFK))
            {
                throw new Exception($"El registro {viewModel.Name} ya se ecuentra registrado.");
            }

            State entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override void Update(int id, StateViewModel viewModel)
        {
            if (_stateRepository.ValidateNameAndCountry(viewModel.Name, viewModel.CountryFK))
            {
                throw new Exception("El registro " + viewModel.Name + " ya se ecuentra registrado.");
            }

            State entity = _repository.GetById(id);

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public override State AddConverter(StateViewModel viewModel)
        {
            return new State()
            {
                Name = viewModel.Name,
                CountryFK = viewModel.CountryFK,
                Active = Constants.True
            };
        }

        public override List<StateViewModel> CollectionConverter(List<State> entities)
        {
            return entities.Select(e => new StateViewModel
            {
                Id = e.Id,
                Name = e.Name,
                CountryFK = e.CountryFK,
                Country = new CountryViewModel()
                {
                    Id = e.CountryFKNavigation.Id,
                    Name = e.CountryFKNavigation.Name,
                }
            }).ToList();
        }

        public override StateViewModel SingleConverter(State entity)
        {
            return new StateViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                CountryFK = entity.CountryFK,
                Country = new CountryViewModel()
                {
                    Id = entity.CountryFKNavigation.Id,
                    Name = entity.CountryFKNavigation.Name
                }
            };
        }

        public override State UpdatedConverter(StateViewModel viewModel, State entity)
        {
            entity.Name = viewModel.Name;
            entity.CountryFK = viewModel.CountryFK;

            return entity;
        }
    }
}
