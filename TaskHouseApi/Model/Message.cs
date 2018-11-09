using System;

namespace TaskHouseApi.Model
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime SendAt { get; set; }

        public Message()
        {
        }
    }
}
