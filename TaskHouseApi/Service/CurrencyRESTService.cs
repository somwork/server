using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaskHouseApi.Model;

namespace TaskHouseApi.Service
{
    public class CurrencyRESTService : ICurrencyRESTService
    {
        private const string URL = "http://data.fixer.io/api/";
        private const string endpoint = "latest";
        private const string accesskey = "07e4663114a3a687e1bf04451e0bf8b7";
        private const string currencySymbols = "&symbols=USD,DKK&format=1";

        private static string currencySymbol;
        private string urlParameters = endpoint + "?access_key=" + accesskey + currencySymbols;
        private string singleCurrencyParameter = endpoint + "?access_key=" + accesskey + "&symbols=" + currencySymbol + "&format=1";

        public CurrencyRESTService() { }

        //Get all currencies with their given properties
        public async Task<Currency> GetCurrenciesAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(urlParameters);
                string res = await client.GetStringAsync(URL + urlParameters);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(res);
                }

                Currency currency = JsonConvert.DeserializeObject<Currency>(
                    await client.GetStringAsync(URL + urlParameters));

                return currency;
            }
        }
    }
}
