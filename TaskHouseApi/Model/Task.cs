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
        public double Urgency { get; set; }
        [JsonIgnore]
        public virtual ICollection<Estimate> Estimates { get; set; }
        [JsonIgnore]
        public virtual Reference Reference { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryTask> CategoryTask { get; set; }
        public int EmployerId { get; set; }
        [JsonIgnore]
        public Employer Employer { get; set; }
        [JsonIgnore]
        public virtual ICollection<Message> Messages { get; set; }
        public double AverageEstimate { get; set; }
        [NotMapped]
        public IDictionary<string, double> UrgencyFactorMap { get; set; }
        [Required]
        public string UrgencyString { get; set; }

        public Task()
        {
            CategoryTask = new List<CategoryTask>();
            Messages = new List<Message>();
            Estimates = new List<Estimate>();
            UrgencyFactorMap = new Dictionary<string, double>();
            UrgencyFactorMap.Add("norush", 1.2);
            UrgencyFactorMap.Add("urgent", 1.4);
            UrgencyFactorMap.Add("asap", 1.5);
        }

        public double CalculateAverageEstimate()
        {
            double SummedEstimate = 0.0;

            if (Estimates.Count <= 0)
            {
                return 0;
            }

            foreach (Estimate e in Estimates)
            {
                SummedEstimate += e.CalculateEstimate();
            }

            return SummedEstimate / Estimates.Count;
        }

        public void MapUrgencyFactor()
        {
            this.Urgency = UrgencyFactorMap[this.UrgencyString];
        }
    }
}
