using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model.ServiceModel
{
    // Model use to send username and password
    // from client
    public class LoginModel
    {

        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
