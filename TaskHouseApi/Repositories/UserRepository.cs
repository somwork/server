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
        protected internal PostgresContext db;

        public UserRepository(PostgresContext db)
        {
            this.db = db;
        }

        public User Create(User u)
        {
            db.Users.Add(u);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return null;
            }
            return u;
        }

        public virtual IEnumerable<User> RetrieveAll()
        {
            return db.Users
                .Include(user => user.RefreshTokens)
                .ToList();
        }

        public virtual User Retrieve(int Id)
        {
            return db.Workers
                .Include(user => user.RefreshTokens)
                .Where(user => user.Id == Id)
                .SingleOrDefault();
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

        public bool Delete(int Id)
        {
            User u = db.Users.Find(Id);
            db.Users.Remove(u);
            int affected = db.SaveChanges();

            if (affected != 1)
            {
                return false;
            }
            return true;
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

        public bool isInDatabase(int Id)
        {
            if (!db.Users.Any(o => o.Id == Id))
            {
                return false;
            }
            return true;
        }
    }
}
