using TaskHouseApi.Model;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeEstimateRepository : FakeRepository<Estimate>, IEstimateRepository
    {
        public FakeEstimateRepository()
            : base(new List<Estimate>()
                {
                    new Estimate()
                    {
                        Id = 1,
                        TotalHours = 10,
                        Complexity = 1,
                        HourlyWage = 110m,
                        Urgency = 1.2m,
                        TaskId = 2,
                    },
                    new Estimate()
                    {
                        Id = 2,
                        TotalHours = 10,
                        Complexity = 1,
                        HourlyWage = 110m,
                        Urgency = 1.5m,
                        TaskId = 2,
                    },
                    new Estimate()
                    {
                        Id = 3,
                        TotalHours = 10,
                        Complexity = 1,
                        HourlyWage = 110m,
                        Urgency = 1.1m,
                        TaskId = 1,
                    },
                    new Estimate()
                    {
                        Id = 4,
                        TotalHours = 10,
                        Complexity = 1,
                        HourlyWage = 110m,
                        Urgency = 1.8m,
                        TaskId = 1,
                    },
                }
            )
        { }
        public IEnumerable<Estimate> RetrieveAllEstimatesForSpecificTaskId(int Id)
        {
            return list.Where(e => e.TaskId == Id).ToList();
        }
    }
}
