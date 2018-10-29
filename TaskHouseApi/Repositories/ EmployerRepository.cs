using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;

public class EmployerRepository : TaskHouseApi.Repositories.IEmployerRepository

{

        public EmployerRepository()
        {
        }


        //cache the Employers in a thread-safe dictionary to improve performance
        private static ConcurrentDictionary<int, Employer> EmployerCache;

        //reference to database context
        private PostgresContext db;

        public EmployerRepository(PostgresContext db)
        {
            this.db = db;

            //populates EmployerCache
            if (EmployerCache == null)
            {
                EmployerCache = new ConcurrentDictionary<int, Employer>(
                    db.Employers.ToDictionary(c => c.Id));
            }
        }

  
    public async Task<Employer> Create(Employer e)
        {
            //add Employer to database using EF core
            EntityEntry<Employer> added = await db.Employers.AddAsync(e);

            int affected = await db.SaveChangesAsync();

            //Employer not created
            if (affected != 1)
            {
                return null;
            }

            // if the customer is new, add it to cache, else call UpdateCache method
            return EmployerCache.AddOrUpdate(e.Id, e, UpdateCache);
        }

        public async Task<IEnumerable<Employer>> RetrieveAll()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<Employer>>(
                () => EmployerCache.Values);
        }

        public async Task<Employer> RetrieveSpecific(LoginModel loginModel)
        {
            return (await RetrieveAll())
                .Single(Employer => Employer.Username == loginModel.Username && Employer.Password == loginModel.Password);
        }

        public async Task<Employer> Retrieve(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Employer u;
                EmployerCache.TryGetValue(Id, out u);
                return u;
            });
        }

        public async Task<Employer> Update(int Id, Employer e)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                db.Employers.Update(e);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }

                return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, e));
            });
        }

        public async Task<bool> Delete(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Employer u = db.Employers.Find(Id);
                db.Employers.Remove(u);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }
                return System.Threading.Tasks.Task.Run(() => EmployerCache.TryRemove(Id, out u));
            });
        }

        private Employer UpdateCache(int Id, Employer e)
        {
            Employer old;
            if (EmployerCache.TryGetValue(Id, out old))
            {
                if (EmployerCache.TryUpdate(Id, e, old))
                {
                    return e;
                }
            }
            return null;
        }

}
