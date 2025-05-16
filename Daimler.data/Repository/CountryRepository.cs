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
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(DAIMLERContext context) : base(context) { }

        public bool ValidateName(string name)
        {
            return _context.Countries.Any(c => c.Name.ToUpper().Trim() == name.ToUpper().Trim());
        }
    }
}
