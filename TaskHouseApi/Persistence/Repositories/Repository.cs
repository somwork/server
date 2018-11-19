using TaskHouseApi.Persistence.Repositories.Interfaces;
using TaskHouseApi.Persistence.Repositories;
using TaskHouseApi.Model;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Collections;

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
            dbSet.Update(baseModel);
        }

        public void UpdatePart(T baseModel, string[] nameOfPropertysToIgnore)
        {
            PropertyInfo[] propertyInfos = baseModel.GetType().GetProperties();
            if (propertyInfos.Count() == 0)
            {
                return;
            }

            bool IsModified = false;
            foreach (PropertyInfo property in propertyInfos)
            {
                /// If the as ICollection or name is Id do nothing
                if (property.GetValue(baseModel) is ICollection || property.Name == "Id")
                {
                    continue;
                }

                /// Attach basemodel
                if (!IsModified)
                {
                    dbSet.Attach(baseModel);
                    IsModified = true;
                }

                /// Gets the property value
                var propertyValue = property.GetValue(baseModel);

                /// If property name is in ignore
                /// or is null
                /// don't change the value
                if (nameOfPropertysToIgnore.Contains(property.Name) || propertyValue == null)
                {
                    context.Entry(baseModel).Property(property.Name).IsModified = false;
                    continue;
                }

                /// Marks the property as modified
                context.Entry(baseModel).Property(property.Name).IsModified = true;
            }
        }
    }
}
