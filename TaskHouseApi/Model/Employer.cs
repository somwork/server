using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Employer : User
    {
        [JsonIgnore]
        public virtual ICollection<Task> Tasks { get; set; }

        public Employer()
        {
            Tasks = new List<Task>();
        }
    }
}
