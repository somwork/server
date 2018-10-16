using System;

namespace TaskHouseApi.Model
{

    public class User
    {
        private string Username { get; set; }
        private string Password { get; set; }
        private int ID { get; set; }
        private string email { get; set; }
        private string firstName { get; set; }
        private string lastName { get; set; }

        public User()
        {
        }
    }
}
