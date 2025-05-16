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
    public class BinnacleManager : BaseManager<BinnacleViewModel, Binnacle>, IBinnacleManager
    {
        public BinnacleManager(IFolderHelper folderHelper, IBinnacleRepository repository) : base(folderHelper, repository) { }

        public override Binnacle AddConverter(BinnacleViewModel viewModel)
        {
            return new Binnacle()
            {
                UserFK = viewModel.UserFK,
                ModuleFK = viewModel.ModuleFK,
                Description = viewModel.Description,
                CreatedAt = DateTime.Now,
                Active = Constants.True
            };
        }
        public override List<BinnacleViewModel> CollectionConverter(List<Binnacle> entities)
        {
            return entities.Select(e => new BinnacleViewModel()
            {

                UserFK = e.UserFK,
                ModuleFK = e.ModuleFK,
                Description = e.Description
            }
            ).ToList();
        }

        public override BinnacleViewModel SingleConverter(Binnacle entity)
        {
            return new BinnacleViewModel()
            {
                Id = entity.Id,
                UserFK = entity.UserFK,
                ModuleFK = entity.ModuleFK,
                Description = entity.Description
            };
        }

        public override Binnacle UpdatedConverter(BinnacleViewModel viewModel, Binnacle entity)
        {
            entity.UserFK = viewModel.UserFK;
            entity.ModuleFK = viewModel.ModuleFK;
            entity.Description = viewModel.Description;

            return entity;
        }
    }
}
