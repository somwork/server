using System;
using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class Education : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [Required]
        public int WorkerId { get; set; }

        public Education()
        {

        }
    }
}
