using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.Repositories;
using TaskHouseApi.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TaskHouseApi.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected internal readonly DbContext context;
        protected internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public T Retrieve(int Id)
        {
            return dbSet.Where(t => t.Id == Id).SingleOrDefault();
        }

        public IEnumerable<T> RetrieveAll()
        {
            return dbSet.ToList();
        }

        public void Create(T baseModel)
        {
            dbSet.Add(baseModel);
        }

        public void Delete(T baseModel)
        {
            if (context.Entry(baseModel).State == EntityState.Detached)
            {
                dbSet.Attach(baseModel);
            }
            dbSet.Remove(baseModel);
        }

        public void Delete(int id)
        {
            T entityToDelete = Retrieve(id);
            dbSet.Remove(entityToDelete);
        }

        public void Update(T baseModel)
        {
            dbSet.Attach(baseModel);
            context.Entry(baseModel).State = EntityState.Modified;
        }
    }
}
