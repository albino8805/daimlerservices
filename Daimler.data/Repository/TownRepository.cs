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
    public class TownRepository : BaseRepository<Town>, ITownRepository
    {
        public TownRepository(DAIMLERContext context) : base(context) { }

        public override IQueryable<Town> GetAll()
        {
            return _context.Towns
                .Include(p => p.StateFKNavigation.CountryFKNavigation)
                .AsQueryable();
        }

        public override Town GetById(int id)
        {
            return _context.Towns
                .Include(e => e.StateFKNavigation.CountryFKNavigation)
                .Where(e => e.Id == id)
                .FirstOrDefault();
        }

        public bool ValidateNameAndState(string name, int stateFK)
        {
            return _context.Towns
                .Any(p => p.Name.ToUpper().Trim() == name.ToUpper().Trim() && p.StateFK == stateFK);
        }
    }
}
