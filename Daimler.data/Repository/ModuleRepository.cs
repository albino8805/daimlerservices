using Daimler.data.IRepository;
using Daimler.data.Models;


namespace Daimler.data.Repository
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(DAIMLERContext context) : base(context) { }

        public bool ValidateName(string name)
        {
            return _context.Modules
                .Any(p => p.Name.ToUpper().Trim() == name.ToUpper().Trim());
        }
    }
}
