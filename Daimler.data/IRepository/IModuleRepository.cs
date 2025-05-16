using System;
using System.Collections.Generic;
using System.Linq;
using Daimler.data.Models;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface IModuleRepository : IBaseRepository<Module>
    {
        bool ValidateName(string name);
    }
}
