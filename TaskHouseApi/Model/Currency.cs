using System;
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
        public DateTime Date { get; set; }

        [JsonProperty("rates")]
        public Rates Rates { get; set; }

        public Currency()
        {
        }
    }
}
