using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Repositories
{
    public interface IWorkerRepository
    {
        Task<User> CreateAsync(User u);

        Task<IEnumerable<User>> RetrieveAllAsync();

        Task<User> RetrieveAsync(int Id);

        Task<User> RetrieveSpecificAsync(LoginModel loginModel);

        Task<User> UpdateAsync(int Id, User u);

        Task<bool> DeleteAsync(int Id);
    }
}
