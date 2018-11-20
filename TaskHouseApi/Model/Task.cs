using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual Reference Reference { get; set; }
        public virtual ICollection<CategoryTask> CategoryTask { get; set; }
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }

        public Task()
        {
            Offers = new List<Offer>();
            CategoryTask = new List<CategoryTask>();
        }
    }
}
