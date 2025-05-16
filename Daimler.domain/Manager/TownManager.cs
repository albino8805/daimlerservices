using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;

namespace Daimler.domain.Manager
{
    public class TownManager : BaseManager<TownViewModel, Town>, ITownManager
    {
        private readonly ITownRepository _townRepository;

        public TownManager(IFolderHelper folderHelper, ITownRepository repository) : base(folderHelper, repository)
        {
            _townRepository = repository;
        }

        public override TownViewModel Add(TownViewModel viewModel)
        {
            if (_townRepository.ValidateNameAndState(viewModel.Name, viewModel.StateFK))
            {
                throw new Exception($"El nombre {viewModel.Name} ya se encuentra registrado.");
            }

            Town entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override void Update(int id, TownViewModel viewModel)
        {
            if (_townRepository.ValidateNameAndState(viewModel.Name, viewModel.StateFK))
            {
                throw new Exception("El nombre " + viewModel.Name + " ya se encuentra registrado.");
            }

            Town entity = _repository.GetById(id);

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public override Town AddConverter(TownViewModel viewModel)
        {
            return new Town()
            {
                Name = viewModel.Name,
                StateFK = viewModel.StateFK,
                Active = Constants.True
            };
        }

        public override List<TownViewModel> CollectionConverter(List<Town> entities)
        {
            return entities.Select(e => new TownViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                StateFK = e.StateFK,
                State = new StateViewModel()
                {
                    Id = e.StateFKNavigation.Id,
                    Name = e.StateFKNavigation.Name,
                    CountryFK = e.StateFKNavigation.CountryFK,
                    Country = new CountryViewModel()
                    {
                        Id = e.StateFKNavigation.CountryFKNavigation.Id,
                        Name = e.StateFKNavigation.CountryFKNavigation.Name
                    }
                }
            }).ToList();
        }

        public override TownViewModel SingleConverter(Town entity)
        {
            return new TownViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                StateFK = entity.StateFK,
                State = new StateViewModel()
                {
                    Id = entity.StateFKNavigation.Id,
                    Name = entity.StateFKNavigation.Name,
                    CountryFK = entity.StateFKNavigation.CountryFK,
                    Country = new CountryViewModel()
                    {
                        Id = entity.StateFKNavigation.CountryFKNavigation.Id,
                        Name = entity.StateFKNavigation.CountryFKNavigation.Name
                    }
                }
            };
        }

        public override Town UpdatedConverter(TownViewModel viewModel, Town entity)
        {
            entity.Name = viewModel.Name;
            entity.StateFK = viewModel.StateFK;

            return entity;
        }
    }
}
