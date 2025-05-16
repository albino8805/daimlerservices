using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;

namespace Daimler.domain.Manager
{
    public class ProfileManager : BaseManager<ProfileViewModel, Profile>, IProfileManager
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IModuleActionManager _moduleActionManager;
        private readonly IModuleActionRepository _moduleActionRepository;

        public ProfileManager(
            IFolderHelper folderHelper,
            IProfileRepository repository,
            IModuleActionManager moduleActionManager,
            IModuleActionRepository moduleActionRepository
            ) : base(folderHelper, repository)
        {
            _profileRepository = repository;
            _moduleActionManager = moduleActionManager;
            _moduleActionRepository = moduleActionRepository;
        }

        public override ProfileViewModel Add(ProfileViewModel viewModel)
        {
            if (_profileRepository.ValidateName(viewModel.Name))
            {
                throw new Exception($"El perfil {viewModel.Name} ya se encuentra registrado.");
            }

            Profile entity = this.AddConverter(viewModel);

            var profile = _repository.Add(entity);

            viewModel.ModuleActions.ForEach(p => p.ProfileFK = profile.Id);

            _moduleActionManager.AddModuleAction(viewModel.ModuleActions);

            return viewModel;
        }

        public override void Update(int id, ProfileViewModel viewModel)
        {
            if (_profileRepository.ValidateName(id, viewModel.Name))
            {
                throw new Exception("El perfil " + viewModel.Name + " ya se encuentra registrado.");
            }

            Profile entity = _repository.GetById(id);

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);

            _moduleActionRepository.ExecuteDelete(id);

            viewModel.ModuleActions.ForEach(p => p.ProfileFK = id);

            _moduleActionManager.AddModuleAction(viewModel.ModuleActions);
        }

        public override void Delete(int id, int userId)
        {
            if (!_profileRepository.HasCustomer(id))
            {
                _profileRepository.Delete(id);
            }
        }

        public override Profile AddConverter(ProfileViewModel viewModel)
        {
            return new Profile()
            {
                Name = viewModel.Name,
                CreatedAt = DateTime.Now,
                Active = Constants.True
            };
        }

        public override List<ProfileViewModel> CollectionConverter(List<Profile> entities)
        {
            return entities.Select(e => new ProfileViewModel()
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
        }

        public override ProfileViewModel SingleConverter(Profile entity)
        {
            return new ProfileViewModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public override Profile UpdatedConverter(ProfileViewModel viewModel, Profile entity)
        {
            entity.Name = viewModel.Name;
            entity.UpdatedAt = DateTime.Now;

            return entity;
        }
    }
}
