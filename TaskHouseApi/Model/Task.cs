using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Task : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Urgency { get; set; }
        [JsonIgnore]
        public virtual ICollection<Offer> Offers { get; set; }
        [JsonIgnore]
        public virtual Reference Reference { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryTask> CategoryTask { get; set; }
        public int EmployerId { get; set; }
        [JsonIgnore]
        public Employer Employer { get; set; }

        public Task()
        {
            Offers = new List<Offer>();
            CategoryTask = new List<CategoryTask>();
        }
    }
}
