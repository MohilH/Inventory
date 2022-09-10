using Inventory.DomainModel.DatabaseModel;
using Inventory.Repositories.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Repositories.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private InventoryEntities _entities = new InventoryEntities();

        protected InventoryEntities Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        public IQueryable<T> GetAll()
        {
            //_entities.Configuration.ProxyCreationEnabled = false;
            IQueryable<T> query = _entities.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }

        public void Add(T entity)
        {
            
            _entities.Set<T>().Add(entity);
            Save();
        }

        public void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
            Save();
        }

        public void Edit(T entity)
        {
            _entities.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _entities.SaveChanges();
        }
    }
}
