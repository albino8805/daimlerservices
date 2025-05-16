using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface ITownRepository : IBaseRepository<Town>
    {
        bool ValidateNameAndState(string name, int stateFK);
    }
}
