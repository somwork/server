using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        //cache the users in a thread-safe dictionary to improve performance
        private static ConcurrentDictionary<int, User> userCache;

        //reference to database context
        private PostgresContext db;

        public UserRepository(PostgresContext db)
        {
            this.db = db;

            //populates userCache
            if (userCache == null)
            {
                userCache = new ConcurrentDictionary<int, User>(
                    db.Users.ToDictionary(c => c.ID));
            }
        }

        public async Task<User> CreateAsync(User u)
        {
            //add user to database using EF core
            EntityEntry<User> added = await db.Users.AddAsync(u);

            int affected = await db.SaveChangesAsync();

            //user not created
            if (affected != 1)
            {
                return null;
            }

            // if the customer is new, add it to cache, else call UpdateCache method
            return userCache.AddOrUpdate(u.ID, u, UpdateCache);
        }

        public async Task<IEnumerable<User>> RetrieveAllAsync()
        {
            return await System.Threading.Tasks.Task.Run<IEnumerable<User>>(
                () => userCache.Values);
        }

        public async Task<User> RetrieveAsync(int ID)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                User u;
                userCache.TryGetValue(ID, out u);
                return u;
            });
        }

        public async Task<User> UpdateAsync(int ID, User u)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                db.Users.Update(u);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }

                return System.Threading.Tasks.Task.Run(() => UpdateCache(ID, u));
            });
        }

        public async Task<bool> DeleteAsync(int ID)
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                User u = db.Users.Find(ID);
                db.Users.Remove(u);
                int affected = db.SaveChanges();

                if (affected != 1)
                {
                    return null;
                }
                return System.Threading.Tasks.Task.Run(() => userCache.TryRemove(ID, out u));
            });
        }

        private User UpdateCache(int ID, User u)
        {
            User old;
            if (userCache.TryGetValue(ID, out old))
            {
                if (userCache.TryUpdate(ID, u, old))
                {
                    return u;
                }
            }
            return null;
        }
    }
}
