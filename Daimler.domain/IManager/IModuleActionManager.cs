using Daimler.data.Models;
using Daimler.data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.domain.IManager
{
    public interface IModuleActionManager : IBaseManager<ModuleActionViewModel, ModuleAction>
    {
        void AddModuleAction(List<ModuleActionViewModel> modelAction);

        List<ModuleDetailViewModel> GetCatalog();
    }
}
