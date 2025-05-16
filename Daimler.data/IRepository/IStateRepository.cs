using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface IStateRepository : IBaseRepository<State>
    {
        bool ValidateNameAndCountry(string name, int countryFK);
    }
}
