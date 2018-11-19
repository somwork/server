using System;
using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeEducationRepository : FakeRepository<Education>, IEducationRepository
    {
        public FakeEducationRepository()
            : base(new List<Education>()
                {
                    new Education()
                    {
                        Id = 1,
                        Title = "edu1",
                        Start = new DateTime(2018, 3, 3),
                        End = new DateTime(2019, 3, 3),
                        WorkerId = 4
                    },
                    new Education()
                    {
                        Id = 2,
                        Title = "edu2",
                        Start = new DateTime(2018, 3, 3),
                        End = new DateTime(2019, 3, 3),
                        WorkerId = 4
                    },
                    new Education()
                    {
                        Id = 3,
                        Title = "edu3",
                        Start = new DateTime(2018, 3, 3),
                        End = new DateTime(2019, 3, 3),
                        WorkerId = 5
                    }
                }
            )
        { }
    }
}
