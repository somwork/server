using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class BaseModel
    {
        public int Id { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string[] nameOfPropertysToIgnore { get; set; }
    }
}
