using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseUnitTests
{
    public class FakeEmpolyersRepository : FakeUserRepository, IEmployerRepository
    {
        public override IEnumerable<User> RetrieveAll()
        {
            return userList.Where(e => e.Discriminator == nameof(Employer));
        }
    }
}


