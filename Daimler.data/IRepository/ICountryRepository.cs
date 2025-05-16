using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface ICountryRepository : IBaseRepository<Country>
    {
        bool ValidateName(string name);
    }
}
