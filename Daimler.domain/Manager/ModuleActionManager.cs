using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;

namespace Daimler.domain.Manager
{
    public class ModuleActionManager : BaseManager<ModuleActionViewModel, ModuleAction>, IModuleActionManager
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly IActionRepository _actionRepository;

        public ModuleActionManager(
            IFolderHelper folderHelper,
            IModuleActionRepository repository,
            IModuleRepository moduleRepository,
            IActionRepository actionRepository
            ) : base(folderHelper, repository)
        {
            _moduleRepository = moduleRepository;
            _actionRepository = actionRepository;
        }

        public override ModuleAction AddConverter(ModuleActionViewModel viewModel)
        {
            return new ModuleAction()
            {
                ModuleFK = viewModel.ModuleFK,
                ActionFK = viewModel.ActionFK,
                ProfileFK = viewModel.ProfileFK
            };
        }

        public override List<ModuleActionViewModel> CollectionConverter(List<ModuleAction> entities)
        {
            return entities.Select(e => new ModuleActionViewModel()
            {
                Id = e.Id,
                ModuleFK = e.ModuleFK,
                ActionFK = e.ActionFK,
                ProfileFK = e.ProfileFK
            }).ToList();
        }

        public override ModuleActionViewModel SingleConverter(ModuleAction entity)
        {
            return new ModuleActionViewModel()
            {
                Id = entity.Id,
                ModuleFK = entity.ModuleFK,
                ActionFK = entity.ActionFK,
                ProfileFK = entity.ProfileFK
            };
        }

        public override ModuleAction UpdatedConverter(ModuleActionViewModel viewModel, ModuleAction entity)
        {
            entity.ModuleFK = viewModel.ModuleFK;
            entity.ActionFK = viewModel.ActionFK;
            entity.ProfileFK = viewModel.ProfileFK;

            return entity;
        }

        public void AddModuleAction(List<ModuleActionViewModel> moduleActions)
        {
            foreach (var moduleAction in moduleActions)
            {
                _repository.Add(new ModuleAction()
                {
                    ModuleFK = moduleAction.ModuleFK,
                    ActionFK = moduleAction.ActionFK,
                    ProfileFK = moduleAction.ProfileFK,
                    Active = true
                });
            }
        }

        public List<ModuleDetailViewModel> GetCatalog()
        {
            var moduleActions = new List<ModuleDetailViewModel>();

            var modules = _moduleRepository.GetAll().ToList();
            var actions = _actionRepository.GetAll().ToList();

            foreach (var module in modules)
            {
                moduleActions.Add(new ModuleDetailViewModel()
                {
                    Module = new ModuleViewModel()
                    {
                        Id = module.Id,
                        Name = module.Name,
                        Description = module.Description
                    },
                    Actions = actions.Select(p => new ActionViewModel()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Checked = false
                    }).ToList()
                });
            }
            return moduleActions;
        }
    }
}
