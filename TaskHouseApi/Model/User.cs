using System;

namespace TaskHouseApi.Model
{

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int ID { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public User()
        {
        }
    }
}
