using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskHouseUnitTests
{
    public class FakeEmpolyerRepository : IEmpolyerRepository
    {
        private static List<Empolyer> EmpolyerCache;

        public FakeUserRepository()
        {
            userCache = new List<User>()
            {
                new Empolyer() 
                { 
                    Id = 1, 
                    Username = "1234", 
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com", 
                    FirstName = "Bob1", 
                    LastName = "Bobsen1", 
                    Salt = "upYKQSsrlub5JAID61/6pA=="
                },
                new Empolyer() 
                { 
                    Id = 2, 
                    Username = "root", 
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com", 
                    FirstName = "Bob2", 
                    LastName = "Bobsen2", 
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug==",
                    Task = (),
                },
                new Empolyer() 
                { 
                    Id = 3, 
                    Username = "hej", 
                    Password = "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", //hej
                    Email = "test@test.com", 
                    FirstName = "Bob3", 
                    LastName = "Bobsen3", 
                    Salt = "U+cUJhQU56X+OCiGF9hb1g=="
                },
            };
        }

        public async Task<Empolyer> CreateAsync(Empolyer e)
        {

            empolyerCache.Add(e);
            return e;
            
        }

        public async Task<IEnumerable<Empolyer>> RetrieveAllAsync()
        {
            return empolyerCache;
            
        }

        public async Task<Empolyer> RetrieveSpecificAsync(LoginModel loginModel)
        {
            return empolyerCache
                .Single(   
                    user => user.Username == loginModel.Username 
                    && user.Password == loginModel.Password
                );
            //return (await RetrieveAllAsync())
            //    .Single(user => user.Username == loginModel.Username && user.Password == loginModel.Password);
        }

        public async Task<User> RetrieveAsync(int Id)
        {
            return userCache.Where(u => u.Id == Id).SingleOrDefault();
            
            //return await System.Threading.Tasks.Task.Run(() =>
            //{
            //    User u;
            //    userCache.TryGetValue(Id, out u);
            //    return u;
            //});
        }

        public async Task<User> UpdateAsync(int Id, User u)
        {
            User old = userCache.Where(user => user.Id == Id).SingleOrDefault();
            int index = userCache.IndexOf(old);

            userCache[index] = u;
            return u;

            //return await System.Threading.Tasks.Task.Run(() =>
            //{
                //db.Users.Update(u);
                //int affected = db.SaveChanges();

                //if (affected != 1)
                //{
                //    return null;
                //}

            //    return System.Threading.Tasks.Task.Run(() => UpdateCache(Id, u));
            //});
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            User user = userCache.Where(u => u.Id == Id).SingleOrDefault();

            if (user == null) return false;

            userCache.Remove(user);

            return true;

            //return await System.Threading.Tasks.Task.Run(() =>
            //{
              //  User user = userCache.Where(u => u.Id == Id);
                //userCache.Remove(user);
                //User u;
                //User u = db.Users.Find(Id);
                //db.Users.Remove(u);
                //int affected = db.SaveChanges();

                //if (affected != 1)
                //{
                //    return null;
                //}
                //return System.Threading.Tasks.Task.Run(() => userCache.TryRemove(Id, out u));
            //});
        }
    }
}
