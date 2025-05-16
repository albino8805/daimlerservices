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
    public class FolderManager : BaseManager<FolderViewModel, Folder>, IFolderManager
    {
        IFolderRepository _folderRepository;
        public FolderManager(IFolderHelper folderHelper, IFolderRepository folderRepository) : base(folderHelper, folderRepository)
        {
            _folderRepository = folderRepository;
        }


        public override FolderViewModel Add(FolderViewModel viewModel)
        {
            _folderHelper.Create(viewModel.Name, viewModel.ParentId ?? 0);

            Folder entity = this.AddConverter(viewModel);

            _repository.Add(entity);

            return viewModel;
        }

        public override FolderViewModel GetById(int id)
        {
            Folder entity = _repository.GetById(id);

            return this.SingleConverter(entity);
        }

        public List<Folder> GetCompletePath(int id)
        {
            List<Folder> folders = new List<Folder>();

            _folderRepository.GetParents(id, folders);

            return folders.OrderBy(p => p.Id).ToList();
        }

        public override Folder AddConverter(FolderViewModel viewModel)
        {
            return new Folder()
            {
                Name = viewModel.Name,
                ParentId = viewModel.ParentId,
                CreatedAt = DateTime.Now,
                CreatedBy = viewModel.CreatedBy,
                Active = Constants.Active
            };
        }

        public override List<FolderViewModel> CollectionConverter(List<Folder> entities)
        {
            if (entities == null)
            {
                return null;
            }

            return entities.Select(e => new FolderViewModel()
            {
                Id = e.Id,
                Name = e.Name,
                ParentId = e.ParentId,
                CreatedAt = e.CreatedAt,
                CreatedBy = e.CreatedBy,
            }).ToList();
        }

        public override FolderViewModel SingleConverter(Folder entity)
        {
            return new FolderViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                ParentId = entity.ParentId,
                CreatedAt = entity.CreatedAt,
                CreatedBy = entity.CreatedBy,
            };
        }
    }
}
