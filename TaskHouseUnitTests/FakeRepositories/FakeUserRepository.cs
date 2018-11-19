using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using System.Linq;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeUserRepository<U> : FakeRepository<U>, IUserRepository<U> where U : User
    {
        public FakeUserRepository(List<U> l) : base(l) { }
        public bool DeleteRefrechToken(RefreshToken refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public bool isInDatabase(int Id)
        {
            return list.Any(u => u.Id == Id);
        }
    }
}

