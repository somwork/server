using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using System.Linq;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class UserRepository<U> : Repository<U>, IUserRepository<U> where U : User
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public UserRepository(PostgresContext db) : base(db) { }

        /*
        *
        *
        * LOKE AT THIS
         */
        public bool DeleteRefrechToken(RefreshToken refreshToken)
        {
            postgresContext.Remove(refreshToken);
            int affected = postgresContext.SaveChanges();
            return affected != 1;
        }

        public bool isInDatabase(int Id)
        {
            return !postgresContext.Users.Any(o => o.Id == Id);
        }
    }
}
