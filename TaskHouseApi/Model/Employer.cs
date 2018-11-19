using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Employer : User
    {
        public virtual ICollection<Task> Tasks { get; set; }

        public Employer()
        {
            Tasks = new List<Task>();
        }
    }
}
