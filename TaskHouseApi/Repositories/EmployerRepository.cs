using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        //reference to database context
        private PostgresContext db;

        public EmployerRepository(PostgresContext db)
        {
            this.db = db;
        }

        public Employer Create(Employer e)
        {
            db.Employers.Add(e);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return null;
            }
            return e;
        }

        public IEnumerable<Employer> RetrieveAll()
        {
            return db.Employers
                .Include(user => user.RefreshTokens)
                .ToList<Employer>();
        }

        public Employer Retrieve(int Id)
        {
            return db.Employers
                .Include(user => user.RefreshTokens)
                .Where(user => user.Id == Id)
                .SingleOrDefault();
        }

        public Employer Update(Employer e)
        {
            db.Employers.Update(e);
            int affected = db.SaveChanges();
            if (affected <= 1)
            {
                return null;
            }
            return e;
        }

        public bool Delete(int Id)
        {
            Employer e = db.Employers.Find(Id);
            db.Employers.Remove(e);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return false;
            }
            return true;
        }
    }
}
