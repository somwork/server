using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositorys
{
    public class FakeReferenceRepository : FakeRepository<Reference>, IReferenceRepository
    {
        public FakeReferenceRepository()
            : base(new List<Reference>()
                {
                    new Reference()
                    {
                        Id = 1,
                        Rating = 4,
                        Statement = "Good",
                        WorkerId = 4,
                        TaskId = 1
                    },
                    new Reference()
                    {
                        Id = 2,
                        Rating = 4,
                        Statement = "Good",
                        WorkerId = 5,
                        TaskId = 2
                    },
                    new Reference()
                    {
                        Id = 3,
                        Rating = 4,
                        Statement = "Good",
                        WorkerId = 4,
                        TaskId = 3
                    },

                }
            )
        { }
    }
}
