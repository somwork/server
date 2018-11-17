using Xunit;
using TaskHouseApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositorys
{
    public class FakeEmployerRepository : FakeUserRepository<Employer>, IEmployerRepository
    {
        public FakeEmployerRepository()
        : base(new List<Employer>()
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
                }
            }
        )
        { }
        // public override IEnumerable<User> RetrieveAll()
        // {
        //     return list.Where(w => w.Discriminator == nameof(Employer));
        // }
    }
}


