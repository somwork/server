using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHouseApi.Model
{
    public class Task : BaseModel
    {
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public string Urgency { get; set; }

        public int EmployerId { get; set; }
        public Employer Employer { get; set; }

        public Task()
        {
        }
    }
}
