using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Daimler.data.ViewModels;
using Daimler.domain.Helpers;
using Daimler.domain.IManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.Manager
{
    public class CountryManager : BaseManager<CountryViewModel, Country>, ICountryManager
    {
        private readonly ICountryRepository _countryRepository;

        public CountryManager(IFolderHelper folderHelper, ICountryRepository repository) : base(folderHelper, repository)
        {
            _countryRepository = repository;
        }

        public override CountryViewModel Add(CountryViewModel viewModel)
        {
            if (_countryRepository.ValidateName(viewModel.Name))
            {
                throw new Exception("El nombre " + viewModel.Name + " ya se encuentra registrado.");
            }

            Country entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override void Update(int id, CountryViewModel viewModel)
        {
            if (_countryRepository.ValidateName(viewModel.Name))
            {
                throw new Exception("El nombre " + viewModel.Name + " ya se encuentra registrado.");
            }

            Country entity = _repository.GetById(id);

            entity = this.UpdatedConverter(viewModel, entity);

            _repository.Update(entity);
        }

        public override Country AddConverter(CountryViewModel viewModel)
        {
            return new Country()
            {
                Name = viewModel.Name,
                Active = Constants.True
            };
        }

        public override List<CountryViewModel> CollectionConverter(List<Country> entities)
        {
            return entities.Select(e => new CountryViewModel()
            {
                Id = e.Id,
                Name = e.Name,
            }).ToList();
        }

        public override CountryViewModel SingleConverter(Country entity)
        {
            return new CountryViewModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public override Country UpdatedConverter(CountryViewModel viewModel, Country entity)
        {
            entity.Name = viewModel.Name;

            return entity;
        }
    }
}
