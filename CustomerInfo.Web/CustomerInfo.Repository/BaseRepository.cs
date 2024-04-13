using CustomerInfo.Entity;
using CustomerInfo.Entity.Model;
using CustomerInfo.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerInfo.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        public CustomerContext DbContext { get; set; }

        public BaseRepository(CustomerContext context)
        {
            DbContext = context;
        }

        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity", "You pass null for inserting");

            var tEntity = entity as BaseEntity;
            // this should be change when you already setup a user
            tEntity.CreatedBy = 1;
            tEntity.CreatedDate = DateTime.Now;


            DbContext.Set<T>().Add(entity);
            return entity;
        }

        public T Update(int id, T entity)
        {
            var found = DbContext.Set<T>().Find(id);

            var tEntity = entity as BaseEntity;
            // this should be change when you already setup a user
            tEntity.ModifiedBy = 1;
            tEntity.ModifiedDate = DateTime.Now;


            if (found != null)
            {
                DbContext.Attach(found);
                DbContext.Entry(found).State = EntityState.Modified;
                DbContext.Entry(found).CurrentValues.SetValues(entity);
            }
            else
                throw new KeyNotFoundException();

            return entity;
        }

        public T Delete(int id)
        {
            var found = DbContext.Set<T>().Find(id);

            if (found != null)
                DbContext.Set<T>().Remove(found);
            else
                throw new KeyNotFoundException();

            return found;
        }

        public T Get(T entity)
        {
            return DbContext.Set<T>().Find(entity);
        }

        public virtual T GetByID(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? param = null)
        {
            if (param != null)
            {
                return DbContext.Set<T>().Where(param);
            }
            else
            {
                return DbContext.Set<T>();
            }
        }
    }
}
