using System;

namespace TaskHouseApi.Model
{
    public class Education : BaseModel
    {
        public string Title { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public int WorkerId { get; set; }

        public Education()
        {

        }
    }
}
