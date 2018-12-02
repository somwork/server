using System;
using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeBudgetRepository : FakeRepository<Budget>, IBudgetRepository
    {
        public FakeBudgetRepository()
            : base(new List<Budget>()
                {
                    new Budget()
                    {
                        Id = 1,
                        From = 50,
                        To = 100,
                        Currency = "EUR",
                    },
                    new Budget()
                    {
                        Id = 2,
                        From = 100,
                        To = 500,
                        Currency = "EUR",
                    },
                    new Budget()
                    {
                        Id = 3,
                        From = 500,
                        To = 1000,
                        Currency = "EUR",
                    }
                }
            )
        { }
    }
}
