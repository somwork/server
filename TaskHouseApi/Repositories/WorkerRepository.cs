using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Repositories;

namespace TaskHouseApi.Repositories
{
    public class WorkerRepository : UserRepository
    {
        public WorkerRepository(PostgresContext db) : base(db)
        {
        }
    }
}
