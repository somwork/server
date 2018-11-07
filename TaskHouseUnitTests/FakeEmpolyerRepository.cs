using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace TaskHouseUnitTests
{
    public class FakeEmppolyerRepository : IEmployersRepository
    {
        private static List<Employer> empolyerCache;

        public FakeEmppolyerRepository()
        {
            empolyerCache = new List<Employer>()
            {
                new Employer()
                {
                    Id = 1,
                    Username = "1234",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "Bob1",
                    LastName = "Bobsen1",
                    Salt = "upYKQSsrlub5JAID61/6pA=="
                    
                },
                new Employer()
                {
                    Id = 2,
                    Username = "root",
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com",
                    FirstName = "Bob2",
                    LastName = "Bobsen2",
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug=="
                },
                new Employer()
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


        public async Task<Employer> Create(Employer e)
        {
            empolyerCache.Add(e);
            return e;
        }


        public async Task<bool> Delete(int Id)
        {
            Employer employer = empolyerCache.Where(s => s.Id == Id).SingleOrDefault();

            if (employer == null) return false;

            empolyerCache.Remove(employer);

            return true;
        }

        public async Task<Employer> Retrieve(int id)
        {
            return empolyerCache.Where(s => s.Id == id).SingleOrDefault();
        }

        public async Task<IEnumerable<Employer>> RetrieveAll()
        {
            return empolyerCache;
        }

      
        public async Task<Employer> Update(int Id, Employer e)
        {
            Employer old = empolyerCache.Where(employer => employer.Id == Id).SingleOrDefault();
            int index = empolyerCache.IndexOf(old);

            empolyerCache[index] = e;
            return e;
        }
    }
}


