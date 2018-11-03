using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User u);

        Task<IEnumerable<User>> RetrieveAllAsync();

        Task<User> RetrieveAsync(int Id);

        Task<User> RetrieveSpecificAsync(LoginModel loginModel);

        Task<User> UpdateAsync(int Id, User u);

        Task<bool> DeleteAsync(int Id);

        bool DeleteRefrechToken(User user, RefreshToken refreshToken);
    }
}
