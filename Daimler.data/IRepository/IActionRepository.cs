using ActionEntity = Daimler.data.Models.Action;

namespace Daimler.data.IRepository
{
    public interface IActionRepository : IBaseRepository<ActionEntity>
    {
        bool ValidateName(string name);
    }
}
