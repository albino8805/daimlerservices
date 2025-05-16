using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;

namespace Daimler.domain.Manager
{
    public class ModuleManager : BaseManager<ModuleViewModel, Module>, IModuleManager
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleManager(IFolderHelper folderHelper, IModuleRepository repository) : base(folderHelper, repository)
        {
            _moduleRepository = repository;
        }

        public override ModuleViewModel Add(ModuleViewModel viewModel)
        {
            if (_moduleRepository.ValidateName(viewModel.Name))
            {
                throw new Exception($"El registro {viewModel.Name} ya se encuentra registrado.");
            }

            Module entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override void Update(int id, ModuleViewModel viewModel)
        {
            if (_moduleRepository.ValidateName(viewModel.Name))
            {
                throw new Exception($"El registro {viewModel.Name} ya se encuentra registrado.");
            }

            Module entity = _repository.GetById(id);

            if (entity == null)
            {
                throw new Exception("No se encontró el registro.");
            }

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public override Module AddConverter(ModuleViewModel viewModel)
        {
            return new Module()
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Active = Constants.True
            };
        }

        public override List<ModuleViewModel> CollectionConverter(List<Module> entities)
        {
            return entities.Select(e => new ModuleViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList();
        }

        public override Module UpdatedConverter(ModuleViewModel viewModel, Module entity)
        {
            entity.Name = viewModel.Name;
            entity.Description = viewModel.Description;

            return entity;
        }

        public override ModuleViewModel SingleConverter(Module entity)
        {
            return new ModuleViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
            };
        }
    }
}
