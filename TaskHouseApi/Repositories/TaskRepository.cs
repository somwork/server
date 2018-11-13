using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        //reference to database context
        private PostgresContext db;


        public TaskRepository(PostgresContext db)
        {
            this.db = db;
        }

        public Task Create(Task t)
        {
            db.Tasks.Add(t);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return null;
            }
            return t;
        }

        public IEnumerable<Task> RetrieveAll()
        {
            return db.Tasks
                .ToList<Task>();
        }

        public Task Retrieve(int Id)
        {
            return db.Tasks
                .Where(task => task.Id == Id)
                .SingleOrDefault();
        }

        public Task Update(Task t)
        {
            db.Tasks.Update(t);
            int affected = db.SaveChanges();
            if (affected <= 1)
            {
                return null;
            }
            return t;
        }

        public bool Delete(int Id)
        {
            Task t = db.Tasks.Find(Id);
            db.Tasks.Remove(t);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return false;
            }
            return true;
        }
    }
}
