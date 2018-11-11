using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> RetrieveAll();
        User Retrieve(int Id);
        User Update(User user);
        bool DeleteRefrechToken(RefreshToken refreshToken);
    }
}
