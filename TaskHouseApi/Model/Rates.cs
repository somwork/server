using System;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Rates : BaseModel
    {
        [JsonProperty("USD")]
        public double Usd { get; set; }

        [JsonProperty("DKK")]
        public double Dkk { get; set; }
    }
}
