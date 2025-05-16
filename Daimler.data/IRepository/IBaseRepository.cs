using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public interface IBaseRepository<E>
    {
        E Add(E entity);
        void Delete(int id, int userId);
        IQueryable<E> GetAll();
        E GetById(int id);
        E Update(E entity);
        void UpdateRange(List<E> entities);
    }
}
