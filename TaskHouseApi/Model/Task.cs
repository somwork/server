using System;
namespace TaskHouseApi.Model
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public String Description { get; set; }
        public String Urgency { get; set; }

        public Task()
        {
        }
    }
}