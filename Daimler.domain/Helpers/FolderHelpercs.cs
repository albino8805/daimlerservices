using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Daimler.data.IRepository;
using Daimler.data.Models;

namespace Daimler.domain.Helpers
{
    public class FolderHelper : IFolderHelper
    {
        private readonly IFolderRepository _folderRepository;

        public FolderHelper(IFolderRepository repository)
        {
            _folderRepository = repository;
        }

        public void Create(string name, int parentId)
        {
            if (parentId == 0)
            {
                if (Validate(name))
                {
                    throw new Exception($"La carpeta '{name}' ya fue creada.");
                }
                else
                {
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Upload\\" + name));
                }
            }
            else
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Upload\\" + this.GetPath(parentId) + "\\" + name));
            }
        }

        public string GetPath(int parentId)
        {
            if (parentId == 0)
            {
                return "";
            }
            else
            {
                Folder folder = _folderRepository.GetById(parentId);

                if (folder == null)
                    return "";

                return GetPath(folder.ParentId ?? 0) + "\\" + folder.Name;
            }
        }

        public string GetCompletePath(int folderId)
        {
            if (folderId == 0)
            {
                return "";
            }
            else
            {
                Folder folder = _folderRepository.GetById(folderId);

                return GetPath(folder.ParentId ?? 0) + "\\" + folder.Name;
            }
        }

        public string GetPathForDownload(int parentFK)
        {
            if (parentFK == 0)
            {
                return "";
            }
            else
            {
                Folder folder = _folderRepository.GetById(parentFK);

                return GetPathForDownload(folder.ParentId ?? 0) + "/" + folder.Name;
            }
        }

        public string GetPathOfFile(int folderId)
        {
            var entity = _folderRepository.GetById(folderId);

            if (entity.ParentId == 0)
            {
                return "/" + entity.Name;
            }
            else
            {
                return GetPathForDownload(entity.ParentId ?? 0) + "/" + entity.Name;
            }
        }

        public static bool Validate(string segmentPath) => Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Upload\\" + segmentPath));
    }
}
