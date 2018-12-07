using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Skill : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public Skill() { }
    }
}
