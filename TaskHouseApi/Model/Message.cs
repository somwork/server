using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Message : BaseModel
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime SendAt { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        [JsonIgnore]
        public int TaskId { get; set; }
        [JsonIgnore]
        public Task Task { get; set; }

        public Message()
        {
        }
    }
}
