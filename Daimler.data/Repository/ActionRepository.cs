using Daimler.data.IRepository;
using Daimler.data.Models;
using ActionEntity = Daimler.data.Models.Action;

namespace Daimler.data.Repository
{
    public class ActionRepository : BaseRepository<ActionEntity>, IActionRepository
    {
        public ActionRepository(DAIMLERContext context) : base(context) { }

        public bool ValidateName(string name)
        {
            return _context.Actions.Any(a => a.Name.ToUpper().Trim() == name.ToUpper().Trim());
        }
    }
}
