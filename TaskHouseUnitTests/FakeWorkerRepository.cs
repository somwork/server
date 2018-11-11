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
    public class FakeWorkerRepository : FakeUserRepository
    {
        public override IEnumerable<User> RetrieveAll()
        {
            return userList.Where(w => w.Discriminator == nameof(Worker));
        }
    }
}
