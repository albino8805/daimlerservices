using Daimler.data.Helpers;
using Daimler.data.IRepository;
using Daimler.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Daimler.data.IRepository
{
    public class BaseRepository<E> : IBaseRepository<E> where E : class
    {
        public readonly DAIMLERContext _context;

        public BaseRepository(DAIMLERContext context)
        {
            _context = context;
        }

        public E Add(E entity)
        {
            _context.Set<E>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(int id, int userId)
        {
            var entity = this.GetById(id);

            GenericObject.TrySetProperty(entity, "Active", false);
            GenericObject.TrySetProperty(entity, "UpdatedBy", userId);
            GenericObject.TrySetProperty(entity, "UpdatedAt", DateTime.Now);

            _context.SaveChanges();
        }

        public virtual IQueryable<E> GetAll()
        {
            return _context.Set<E>().AsQueryable();
        }

        public virtual E GetById(int id)
        {
            return _context.Set<E>().Find(id);
        }

        public E Update(E entity)
        {
            _context.Set<E>().Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public void UpdateRange(List<E> entities)
        {
            _context.Set<E>().UpdateRange(entities);
            _context.SaveChanges();
        }
    }
}
