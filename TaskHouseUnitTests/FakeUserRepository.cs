using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseUnitTests
{
    public class FakeUserRepository : IUserRepository
    {
        private static List<User> userCache;

        public FakeUserRepository()
        {
            userCache = new List<User>()
            {
                new User() 
                { 
                    Id = 1, 
                    Username = "1234", 
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com", 
                    FirstName = "Bob1", 
                    LastName = "Bobsen1", 
                    Salt = "upYKQSsrlub5JAID61/6pA=="
                },
                new User() 
                { 
                    Id = 2, 
                    Username = "root", 
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com", 
                    FirstName = "Bob2", 
                    LastName = "Bobsen2", 
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug=="
                },
                new User() 
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

        public async Task<User> CreateAsync(User u)
        {
            userCache.Add(u);
            return u;
        }

        public async Task<IEnumerable<User>> RetrieveAllAsync()
        {
            return userCache;
        }

        public async Task<User> RetrieveSpecificAsync(LoginModel loginModel)
        {
            return userCache
                .Single(   
                    user => user.Username == loginModel.Username 
                    && user.Password == loginModel.Password
                );
        }

        public async Task<User> RetrieveAsync(int Id)
        {
            return userCache.Where(u => u.Id == Id).SingleOrDefault();
        }

        public async Task<User> UpdateAsync(int Id, User u)
        {
            User old = userCache.Where(user => user.Id == Id).SingleOrDefault();
            int index = userCache.IndexOf(old);

            userCache[index] = u;
            return u;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            User user = userCache.Where(u => u.Id == Id).SingleOrDefault();

            if (user == null) return false;

            userCache.Remove(user);

            return true;
        }


        public bool DeleteRefrechToken(User user, RefreshToken refreshToken)
        {
            User us = userCache.Where(u => u.Id == user.Id).SingleOrDefault();

            if (us == null) return false;

            us.RefreshTokens.Remove(refreshToken);

            return true;
        }
    }
}
