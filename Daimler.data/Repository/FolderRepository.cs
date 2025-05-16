using Daimler.data.IRepository;
using Daimler.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Repository
{
    public class FolderRepository : BaseRepository<Folder>, IFolderRepository
    {
        public FolderRepository(DAIMLERContext context) : base(context)
        {
        }

        public List<Folder> GetParents(int? id, List<Folder> folders)
        {
            var entity = _context.Folders.FirstOrDefault(p => p.Id == id);

            if (entity != null)
            {
                folders.Add(entity);

                if (entity.ParentId != null)
                {
                    GetParents(entity.ParentId, folders);
                }
            }

            return folders;
        }
    }
}
