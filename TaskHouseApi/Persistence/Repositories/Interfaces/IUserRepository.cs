using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Persistence.Repositories.Interfaces
{
    public interface IUserRepository<U> : IRepository<U> where U : User
    {
        bool isInDatabase(int Id);

        bool DeleteRefrechToken(RefreshToken refreshToken);
    }
}
