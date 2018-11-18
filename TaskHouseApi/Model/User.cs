using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Model
{
    public abstract class User : BaseModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
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
