using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private PostgresContext db;

        public WorkerRepository(PostgresContext db)
        {
            this.db = db;
        }
        public Worker Create(Worker w)
        {
            db.Workers.Add(w);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return null;
            }
            return w;
        }

        public IEnumerable<Worker> RetrieveAll()
        {
            return db.Workers
                .Include(user => user.RefreshTokens)
                .ToList<Worker>();
        }

        public Worker Retrieve(int Id)
        {
            return db.Workers
                .Include(user => user.RefreshTokens)
                .Where(user => user.Id == Id)
                .SingleOrDefault();
        }

        public Worker Update(Worker u)
        {
            db.Workers.Update(u);
            int affected = db.SaveChanges();
            if (affected <= 1)
            {
                return null;
            }
            return u;
        }

        public bool Delete(int Id)
        {
            Worker u = db.Workers.Find(Id);
            db.Workers.Remove(u);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return false;
            }
            return true;
        }
    }
}
