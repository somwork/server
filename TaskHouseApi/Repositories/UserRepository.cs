using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using System.Linq;

namespace TaskHouseApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private PostgresContext db;

        public UserRepository(PostgresContext db)
        {
            this.db = db;
        }

        public bool DeleteRefrechToken(RefreshToken refreshToken)
        {
            db.Remove(refreshToken);
            int affected = db.SaveChanges();
            if (affected != 1)
            {
                return false;
            }
            return true;
        }

        public User Retrieve(int Id)
        {
            return db.Workers
                .Include(user => user.RefreshTokens)
                .Where(user => user.Id == Id)
                .SingleOrDefault();
        }

        public IEnumerable<User> RetrieveAll()
        {
            return db.Users
                .Include(user => user.RefreshTokens)
                .ToList();
        }

        public User Update(User user)
        {
            db.Users.Update(user);
            int affected = db.SaveChanges();
            if (affected <= 1)
            {
                return null;
            }
            return user;
        }
    }
}
