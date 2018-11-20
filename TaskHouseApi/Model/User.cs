using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using TaskHouseApi.Model.ServiceModel;
using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public abstract class User : BaseModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public string Discriminator { get; set; }
        public Location Location { get; set; }

        public User()
        {
            RefreshTokens = new List<RefreshToken>();
        }
    }
}
