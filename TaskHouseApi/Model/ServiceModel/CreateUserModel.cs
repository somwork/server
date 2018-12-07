using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class CreateUserModel<U> where U : User
    {
        [Required]
        public U User { get; set; }
        [Required]
        public string Password { get; set; }
        public CreateUserModel() { }
    }
}
