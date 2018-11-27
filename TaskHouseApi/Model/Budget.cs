using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class Budget : BaseModel
    {
        [Required]
        public decimal From { get; set; }
        [Required]
        public decimal To { get; set; }
        [Required]
        public string Currency { get; set; }

        public Budget()
        {
        }
    }
}
