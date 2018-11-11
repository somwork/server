using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Repositories;
using System.Linq;

namespace TaskHouseUnitTests
{
    public class FakeUserRepository : IUserRepository
    {
        protected internal List<User> userList;

        public FakeUserRepository()
        {
            userList = new List<User>()
            {
                new Employer()
                {
                    Id = 1,
                    Username = "1234",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "Bob1",
                    LastName = "Bobsen1",
                    Salt = "upYKQSsrlub5JAID61/6pA==",
                    Discriminator = "Employer"

                },
                new Employer()
                {
                    Id = 2,
                    Username = "root",
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com",
                    FirstName = "Bob2",
                    LastName = "Bobsen2",
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug==",
                    Discriminator = "Employer"
                },
                new Employer()
                {
                    Id = 3,
                    Username = "hej",
                    Password = "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", //hej
                    Email = "test@test.com",
                    FirstName = "Bob3",
                    LastName = "Bobsen3",
                    Salt = "U+cUJhQU56X+OCiGF9hb1g==",
                    Discriminator = "Employer"
                },
                new Worker()
                {
                    Id = 4,
                    Username = "w1",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "Bob1",
                    LastName = "Bobsen1",
                    Salt = "upYKQSsrlub5JAID61/6pA==",
                    Discriminator = "Worker"
                },
                new Worker()
                {
                    Id = 5,
                    Username = "w2",
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com",
                    FirstName = "Bob2",
                    LastName = "Bobsen2",
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug==",
                    Discriminator = "Worker"
                },
                new Worker()
                {
                    Id = 6,
                    Username = "w3",
                    Password = "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", //hej
                    Email = "test@test.com",
                    FirstName = "Bob3",
                    LastName = "Bobsen3",
                    Salt = "U+cUJhQU56X+OCiGF9hb1g==",
                    Discriminator = "Worker"
                },
            };
        }

        public User Create(User u)
        {
            userList.Add(u);
            return u;
        }

        public bool Delete(int Id)
        {
            User temp = userList.Find(u => u.Id == Id);
            userList.Remove(temp);
            temp = userList.Find(u => u.Id == Id);

            if (temp != null)
            {
                return false;
            }
            return true;
        }

        public bool DeleteRefrechToken(RefreshToken refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public bool isInDatabase(int Id)
        {
            return userList.Any(u => u.Id == Id);
        }

        public virtual User Retrieve(int Id)
        {
            return userList.Find(u => u.Id == Id);
        }

        public virtual IEnumerable<User> RetrieveAll()
        {
            return userList;
        }

        public User Update(User u)
        {
            User old = Retrieve(u.Id);
            int index = userList.IndexOf(old);

            userList[index] = u;
            return u;
        }
    }
}
