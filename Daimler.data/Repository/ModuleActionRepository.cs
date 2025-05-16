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
    public class ModuleActionRepository : BaseRepository<ModuleAction>, IModuleActionRepository
    {
        public ModuleActionRepository(DAIMLERContext context) : base(context) { }

        public void ExecuteDelete(int id)
        {
            _context.ModuleActions.Where(c => c.ProfileFK == id).ExecuteDelete();
            _context.SaveChanges();
        }

        public List<ModuleAction> GetByProfile(int profileFK)
        {
            return _context.ModuleActions
                .Where(s => s.ProfileFK == profileFK)
                .ToList();
        }
    }
}
