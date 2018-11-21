using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Currency : BaseModel
    {

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }

        public Currency()
        {
        }
    }

    public partial class Rates
    {
        [JsonProperty("USD")]
        public double Usd { get; set; }

        [JsonProperty("DKK")]
        public double Dkk { get; set; }
    }
}
