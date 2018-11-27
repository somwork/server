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
    public abstract class Repository<T> : IRepository<T> where T : BaseModel
    {
        protected internal readonly DbContext context;
        protected internal DbSet<T> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual T Retrieve(int Id)
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

        public virtual void UpdatePart(T baseModel, string[] nameOfPropertysToIgnore)
        {
            PropertyInfo[] propertyInfos = baseModel.GetType().GetProperties();
            if (propertyInfos.Count() == 0)
            {
                return;
            }

            bool IsModified = false;
            foreach (PropertyInfo property in propertyInfos)
            {

                /// If the name is Id or the propertyname should be ignored or the type of a Reference do nothing
                if (
                    property.Name == "Id" ||
                    nameOfPropertysToIgnore.Contains(property.Name) ||
                    property.PropertyType == typeof(Reference)
                )
                {
                    continue;
                }

                /// Gets the property value
                var propertyValue = property.GetValue(baseModel);

                /// Attach basemodel
                if (!IsModified)
                {
                    dbSet.Attach(baseModel);
                    IsModified = true;
                }

                /// If property name is in ignore
                /// or is null
                /// don't change the value
                if (nameOfPropertysToIgnore.Contains(property.Name) || propertyValue == null)
                {
                    continue;
                }

                /// If property is a ICollection
                /// don't change the value
                if (property.PropertyType == typeof(ICollection))
                {
                    context.Entry(baseModel).Collection(property.Name).IsModified = false;
                }

                /// If property is is a string
                /// change the value
                if (
                    property.PropertyType == typeof(string) ||
                    property.PropertyType.IsValueType
                )
                {
                    /// Marks the property as modified
                    context.Entry(baseModel).Property(property.Name).IsModified = true;
                }
            }
        }
    }
}
