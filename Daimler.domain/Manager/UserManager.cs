using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;

namespace Daimler.domain.Manager
{
    public class UserManager : BaseManager<UserViewModel, User>, IUserManager
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IFolderHelper folderHelper, IUserRepository repository) : base(folderHelper, repository)
        {
            _userRepository = repository;
        }

        public void UpdatePassword(PasswordViewModel viewModel)
        {
            User entity = _repository.GetById(viewModel.UserId);

            entity.Password = viewModel.NewPassword;

            _repository.Update(entity);
        }

        public override UserViewModel Add(UserViewModel viewModel)
        {
            if (_userRepository.ValidateEmail(viewModel.Email))
            {
                throw new Exception($"El correo '{viewModel.Email}' ya se encuentra registrado.");
            }

            User entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override void Update(int id, UserViewModel viewModel)
        {
            if (_userRepository.ValidateIdAndEmail(id, viewModel.Email))
            {
                throw new Exception($"El correo '{viewModel.Email}' ya se encuentra registrado.");
            }

            User entity = _repository.GetById(id);

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public override User AddConverter(UserViewModel viewModel)
        {
            return new User()
            {
                Name = viewModel.Name,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Password = viewModel.Password,
                Phone = viewModel.Phone,
                ProfileFK = viewModel.ProfileFK,
                CreatedAt = DateTime.Now,
                Active = Constants.True
            };
        }

        public override List<UserViewModel> CollectionConverter(List<User> entities)
        {
            if (entities.Count == 0)
            {
                throw new Exception("No existen coincidencias!");
            }

            return entities.Select(e => new UserViewModel
            {
                Id = e.Id,
                Name = e.Name,
                LastName = e.LastName,
                Email = e.Email,
                Phone = e.Phone,
                ProfileFK = e.ProfileFK,
                Profile = new ProfileViewModel()
                {
                    Id = e.ProfileFKNavigation.Id,
                    Name = e.ProfileFKNavigation.Name
                }
            }).ToList();
        }

        public override UserViewModel SingleConverter(User entity)
        {
            return new UserViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                ProfileFK = entity.ProfileFK,
                Profile = new ProfileViewModel()
                {
                    Id = entity.ProfileFKNavigation.Id,
                    Name = entity.ProfileFKNavigation.Name
                }
            };
        }

        public override User UpdatedConverter(UserViewModel viewModel, User entity)
        {
            entity.Name = viewModel.Name;
            entity.LastName = viewModel.LastName;
            entity.Email = viewModel.Email;
            entity.Phone = viewModel.Phone;
            entity.ProfileFK = viewModel.ProfileFK;
            entity.UpdatedAt = DateTime.Now;

            return entity;
        }
    }
}
