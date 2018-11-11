using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Repositories
{
    public class EmployerRepository : UserRepository
    {

        public EmployerRepository(PostgresContext db) : base(db) { }

        public override IEnumerable<User> RetrieveAll()
        {
            return db.Employers
                .Include(user => user.RefreshTokens)
                .Include(user => user.Tasks)
                .ToList<Employer>();
        }

        public override User Retrieve(int Id)
        {
            return db.Employers
                .Include(user => user.RefreshTokens)
                .Include(user => user.Tasks)
                .Where(user => user.Id == Id)
                .SingleOrDefault();
        }
    }
}
