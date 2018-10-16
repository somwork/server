using System;

namespace TaskHouseApi.Model
{

    public class Message
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime SendAt { get; set; }

        public Message()
        {
        }
    }
}
