using System;
using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeCurrencyRepository : FakeRepository<Currency>, ICurrencyRepository
    {
        public FakeCurrencyRepository()
            : base(new List<Currency>()
                {
                    new Currency()
                    {
                        Id = 1,
                        Success = true,
                        Timestamp = 150237234,
                        Base = "EUR",
                        Rates = new Rates(){
                            Usd = 1.133652,
                            Dkk = 7.461019
                        }
                    }
                }
            )
        { }
    }
}