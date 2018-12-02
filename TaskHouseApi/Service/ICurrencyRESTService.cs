using System.Threading.Tasks;
using TaskHouseApi.Model;

namespace TaskHouseApi.Service
{
    public interface ICurrencyRESTService
    {
        Task<Currency> GetCurrenciesAsync();
    }
}
