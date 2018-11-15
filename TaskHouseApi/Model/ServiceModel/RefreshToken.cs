using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHouseApi.Model.ServiceModel
{
    public class RefreshToken : BaseModel
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public RefreshToken() { }
    }
}
