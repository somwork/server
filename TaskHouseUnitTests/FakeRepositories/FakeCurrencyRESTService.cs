using System;
using System.Threading.Tasks;
using TaskHouseApi.Model;
using TaskHouseApi.Service;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeCurrencyRESTService : ICurrencyRESTService
    {
        public async Task<Currency> GetCurrenciesAsync()
        {
            return await System.Threading.Tasks.Task.FromResult<Currency>(
                new Currency()
                {
                    Success = true,
                    Timestamp = 1543763046,
                    Base = "EUR",
                    Date = new DateTime(),
                    Rates = new Rates()
                    {
                        Usd = 1.131753,
                        Dkk = 7.462558
                    }
                }
            );
        }
    }
}
