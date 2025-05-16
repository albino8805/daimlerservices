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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DAIMLERContext context) : base(context) { }

        public User GetByEmail(string email)
        {
            return _context.Set<User>().Where(p => p.Email.ToUpper().Trim() == email.ToUpper().Trim() && p.Active).FirstOrDefault();
        }

        public bool ValidateEmail(string email)
        {
            return _context.Set<User>().Any(p => p.Email.ToUpper().Trim() == email.ToUpper().Trim() && p.Active);
        }

        public User GetByEmailAndPhone(string email, string phone)
        {
            return _context.Set<User>().Where(p => p.Email.ToUpper().Trim() == email.ToUpper().Trim() && p.Phone.ToUpper().Trim() == phone.ToUpper().Trim() && p.Active).FirstOrDefault();
        }

        public User GetByEmailOrPhone(string email, string phone)
        {
            return _context.Set<User>().Where(p => p.Email.ToUpper().Trim() == email.ToUpper().Trim() || p.Phone.ToUpper().Trim() == phone.ToUpper().Trim() && p.Active).FirstOrDefault();
        }

        public override IQueryable<User> GetAll()
        {
            return _context.Users
                .Include(e => e.ProfileFKNavigation)
                .AsQueryable();
        }

        public override User GetById(int id)
        {
            return _context.Users
                .Include(e => e.ProfileFKNavigation)
                .Where(e => e.Id == id)
                .FirstOrDefault();
        }

        public bool ValidateIdAndEmail(int id, string email)
        {
            return _context.Users.Any(n => n.Id != id && n.Email.ToUpper().Trim() == email.ToUpper().Trim() && n.Active);
        }
    }
}
