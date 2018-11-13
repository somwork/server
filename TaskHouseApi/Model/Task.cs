using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHouseApi.Model
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public string Urgency { get; set; }

        [ForeignKey("EmployerId")]
        public int EmployerId { get; set; }

        public Task()
        {
        }
    }
}
