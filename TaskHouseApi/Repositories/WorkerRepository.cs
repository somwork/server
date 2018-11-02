using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        //reference to database context
        private PostgresContext db;

        
        public WorkerRepository(PostgresContext db){
            this.db=db;
        }
        

        public async System.Threading.Tasks.Task Create(Worker w)
        {
            //add user to database using EF core
            EntityEntry<Worker> added = await db.Workers.AddAsync(w);

            int affected = await db.SaveChangesAsync();
           
        }

        public async Task<IEnumerable<Worker>> RetrieveAll()
        {
            var WorkerArray = await System.Threading.Tasks.Task.Run<IEnumerable<Worker>>(
                () => db.Workers.ToArray());
            return WorkerArray;
        }

        public async Task<Worker> RetrieveSpecific(LoginModel loginModel)
        {
            return (await RetrieveAll())
                .Single(user => user.Username == loginModel.Username && user.Password == loginModel.Password);
        }

        public async Task<Worker> Retrieve(int Id)
        {
            return await System.Threading.Tasks.Task.Run(()=>db.Workers.Find(Id));
            }
        

        public async Task<Worker> Update(int Id, Worker u)
        {
           await System.Threading.Tasks.Task.Run(()=>{
            Worker target = db.Workers.Find(Id);
            db.Entry(target).CurrentValues.SetValues(u);
            int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }
                return u;
            });
            return null;
        }

        public async Task<bool> Delete(int Id)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                Worker u = db.Workers.Find(Id);
                db.Workers.Remove(u);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return false;
                }
              return true;
            });
        }

    }
}
