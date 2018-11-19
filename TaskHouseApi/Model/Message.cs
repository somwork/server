using System;

namespace TaskHouseApi.Model
{
    public class Message : BaseModel
    {
        public string Text { get; set; }
        public DateTime SendAt { get; set; }

        public Message()
        {
        }
    }
}
