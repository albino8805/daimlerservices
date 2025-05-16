using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetByEmail(string email);

        bool ValidateEmail(string email);

        bool ValidateIdAndEmail(int id, string name);

        User GetByEmailAndPhone(string email, string phone);

        User GetByEmailOrPhone(string email, string phone);
    }
}
