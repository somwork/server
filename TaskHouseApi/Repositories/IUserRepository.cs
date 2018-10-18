using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User u);

        Task<IEnumerable<User>> RetrieveAllAsync();

        Task<User> RetrieveAsync(int ID);

        Task<User> UpdateAsync(int ID, User u);

        Task<bool> DeleteAsync(int ID);
    }
}
