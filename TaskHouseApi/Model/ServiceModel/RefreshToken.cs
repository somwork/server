using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHouseApi.Model.ServiceModel
{
    public class RefreshToken : BaseModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public User User { get; set; }

        public RefreshToken() { }
    }
}
