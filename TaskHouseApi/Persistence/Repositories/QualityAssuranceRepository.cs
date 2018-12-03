using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using TaskHouseApi.Persistence.DatabaseContext;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseApi.Persistence.Repositories
{
    public class QualityAssuranceRepository : UserRepository<QualityAssurance>, IQualityAssuranceRepository
    {
        public QualityAssuranceRepository(PostgresContext db) : base(db) { }
    }
}
