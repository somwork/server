using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using System.Linq;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class UserRepository<U> : Repository<U>, IUserRepository<U> where U : User
    {
        // Mappes context to postgresContext
        protected internal PostgresContext postgresContext { get { return context as PostgresContext; } }

        public UserRepository(PostgresContext db) : base(db) { }

        public override U Retrieve(int Id)
        {
            return dbSet.Where(t => t.Id == Id)
                .Include(u => u.RefreshTokens)
                .SingleOrDefault();
        }

        public override void UpdatePart(U baseModel, string[] nameOfPropertysToIgnore)
        {
            base.UpdatePart(baseModel, nameOfPropertysToIgnore);
        }

        public void UpdatePart(U baseModel)
        {
            var nameOfPropertysToIgnore = new string[] { "Password", "Salt", "RefreshTokens", "Discriminator" };
            UpdatePart(baseModel, nameOfPropertysToIgnore);
        }

        public bool DeleteRefrechToken(RefreshToken refreshToken)
        {
            postgresContext.Remove(refreshToken);
            int affected = postgresContext.SaveChanges();
            return affected != 1;
        }

    }
}
