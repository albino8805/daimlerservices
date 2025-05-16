using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface IModuleActionRepository : IBaseRepository<ModuleAction>
    {
        void ExecuteDelete(int id);

        List<ModuleAction> GetByProfile(int profileFK);
    }
}
