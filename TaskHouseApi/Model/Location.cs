using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class Location : BaseModel
    {
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string PrimaryLine { get; set; }
        [Required]
        public string SecondaryLine { get; set; }
        [Required]
        public int UserId { get; set; }

        public Location()
        {

        }
    }
}

