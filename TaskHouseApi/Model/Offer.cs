using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Offer : BaseModel
    {
        [Required]
        public bool Accepted { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int TotalHours { get; set; }
        [Required]
        public int Complexity { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int TaskId { get; set; }

        public Offer() { }
    }
}
