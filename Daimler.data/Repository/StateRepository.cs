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
    public class StateRepository : BaseRepository<State>, IStateRepository
    {
        public StateRepository(DAIMLERContext context) : base(context) { }

        public override IQueryable<State> GetAll()
        {
            return _context.States
                .Include(p => p.CountryFKNavigation)
                .AsQueryable();
        }

        public override State GetById(int id)
        {
            return _context.States
                .Include(e => e.CountryFKNavigation)
                .Where(e => e.Id == id)
                .FirstOrDefault();
        }

        public bool ValidateNameAndCountry(string name, int countryFK)
        {
            return _context.States
                .Any(p => p.Name.ToUpper().Trim() == name.ToUpper().Trim() && p.CountryFK == countryFK);
        }
    }
}
