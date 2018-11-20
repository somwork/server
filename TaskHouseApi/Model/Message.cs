using System;
using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class Message : BaseModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime SendAt { get; set; }

        public Message()
        {
        }
    }
}
