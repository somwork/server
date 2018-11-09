using System;
namespace TaskHouseApi.Model
{
    public class Task
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public string Urgency { get; set; }

        public Task()
        {
        }
    }
}
