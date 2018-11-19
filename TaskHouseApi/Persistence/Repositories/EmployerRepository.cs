using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class EmployerRepository : UserRepository<Employer>, IEmployerRepository
    {

        public EmployerRepository(PostgresContext db) : base(db) { }
    }
}
