using TaskHouseApi.Model;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeQualityAssuranceRepository : FakeUserRepository<QualityAssurance>, IQualityAssuranceRepository
    {
        public FakeQualityAssuranceRepository()
       : base(new List<QualityAssurance>()
       {
                new QualityAssurance()
                {
                    Id = 7,
                    Username = "q1",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "Bob1",
                    LastName = "Bobsen1",
                    Salt = "upYKQSsrlub5JAID61/6pA==",
                    Discriminator = "QualityAssurance"
                },
                new QualityAssurance()
                {
                    Id = 8,
                    Username = "q2",
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com",
                    FirstName = "Bob2",
                    LastName = "Bobsen2",
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug==",
                    Discriminator = "QualityAssurance"
                },
                new QualityAssurance()
                {
                    Id = 9,
                    Username = "q3",
                    Password = "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", //hej
                    Email = "test@test.com",
                    FirstName = "Bob3",
                    LastName = "Bobsen3",
                    Salt = "U+cUJhQU56X+OCiGF9hb1g==",
                    Discriminator = "QualityAssurance"
                }
       }
       )
        { }
    }
}
