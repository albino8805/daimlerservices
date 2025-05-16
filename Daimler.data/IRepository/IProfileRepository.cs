using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        bool ValidateName(string name);
        bool ValidateName(int id, string name);
        bool HasCustomer(int id);
        void Delete(int id);
    }
}
