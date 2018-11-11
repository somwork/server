using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Repositories
{
    public interface IUserRepository
    {
        User Create(User u);

        IEnumerable<User> RetrieveAll();

        User Retrieve(int Id);

        User Update(User u);

        bool Delete(int Id);

        bool isInDatabase(int Id);

        bool DeleteRefrechToken(RefreshToken refreshToken);
    }
}
