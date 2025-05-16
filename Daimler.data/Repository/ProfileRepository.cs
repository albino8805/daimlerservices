using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.Repository
{
    public class ProfileRepository : BaseRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(DAIMLERContext context) : base(context) { }

        public bool ValidateName(string name)
        {
            return _context.Profiles.Any(p => p.Name.ToUpper().Trim() == name.ToUpper().Trim() && p.Active);
        }

        public bool ValidateName(int id, string name)
        {
            return _context.Profiles.Any(p => p.Id != id && p.Name.ToUpper().Trim() == name.ToUpper().Trim() && p.Active);
        }

        public bool HasCustomer(int id)
        {
            return _context.Users.Any(p => p.ProfileFK == id && p.Active);
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id);

            entity.Active = Constants.Deactive;
            entity.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
        }
    }
}
