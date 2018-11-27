using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Reference : BaseModel
    {
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Statement { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int TaskId { get; set; }

        public Reference() { }
    }
}
