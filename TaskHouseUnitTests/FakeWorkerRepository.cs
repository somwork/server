using TaskHouseApi.Model;
using TaskHouseApi.Repositories;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
namespace TaskHouseUnitTests
{
    public class FakeWorkerRepository : IWorkerRepository
    {
        private static List<Worker> db;

        public FakeWorkerRepository()
        {
            db = new List<Worker>()
            {
                new Worker()
                {
                    Id = 1,
                    Username = "1234",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "Bob1",
                    LastName = "Bobsen1",
                    Salt = "upYKQSsrlub5JAID61/6pA=="
                },
                new Worker()
                {
                    Id = 2,
                    Username = "root",
                    Password = "gCekoOdivUyOosjAz2wP9X+8TEEpe/LWJekDuvXV8bQ=", //root
                    Email = "test@test.com",
                    FirstName = "Bob2",
                    LastName = "Bobsen2",
                    Salt = "Ci1Zm+9HbvPCvVpBLcSFug=="
                },
                new Worker()
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

        public async Task<Worker> Create(Worker w)
        {
           db.Add(w);
           return w;
        }

        public async Task<IEnumerable<Worker>> RetrieveAll()
        {
            return db;
        }

        public async Task<Worker> Retrieve(int Id)
        {
            return  db.Find(w => w.Id==Id);
        }

        public async Task<Worker> Update(int Id, Worker w)
        {
            var temp = db.Find(i => i.Id == Id);
            db.Remove(temp);
            db.Add(w);
            return db.Find(j => j.Id==Id);
        }

        public async Task<bool> Delete(int Id)
        {
            bool result;

            Worker temp = db.Find(w => w.Id == Id);
            db.Remove(temp);
            temp =  db.Find(w => w.Id==Id);

            if(temp == null)
            {
                return false;
            }

            return true;
        }

    }

}
