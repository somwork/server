using System;

namespace TaskHouseApi.Model
{
    public class Education
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Education()
        {

        }
    }
}
