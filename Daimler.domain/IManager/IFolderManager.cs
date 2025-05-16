using Daimler.data.Models;
using Daimler.data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.IManager
{
    public interface IFolderManager : IBaseManager<FolderViewModel, Folder>
    {
        List<Folder> GetCompletePath(int id);
    }
}
