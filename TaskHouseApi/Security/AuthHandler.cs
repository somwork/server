using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Security
{
    public class AuthHandler
    {
        private static IUserRepository repo;

        public AuthHandler(IUserRepository repo)
        {
            this.repo = repo;
        }

        public static async Task<User> Authenticate(LoginModel login)
        {
            return (await repo.RetrieveAllAsync())
                .Where(user => user.Username == username);

            User user = null;

            if (login.Username == "mario" && login.Password == "secret")
            {
                user = new User { Name = "Mario Rossi", Email = "mario.rossi@domain.com" };
            }
            return user;

            return (repo.RetrieveAllAsync()).Where(user => );
        }
    }
}
