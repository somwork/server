﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHouseApi.Model
{

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salt { get; set; }

        public User()
        {
        }
    }
}
