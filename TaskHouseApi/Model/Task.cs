﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public decimal Urgency { get; set; }
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
        public decimal AverageEstimate { get; set; }

        public IDictionary<string, decimal> UrgencyFactorMap { get; set; }

        [Required]
        public string UrgencyString {
            get {
                return this.UrgencyString;
            }
            set {
                this.UrgencyString = value;
                this.Urgency = UrgencyFactorMap[value];
            }
        }

        public Task()
        {
            CategoryTask = new List<CategoryTask>();
            Messages = new List<Message>();
            Estimates = new List<Estimate>();
            UrgencyFactorMap = new Dictionary<string, decimal>();
            UrgencyFactorMap.Add("norush", 1.2M);
            UrgencyFactorMap.Add("urgent", 1.4M);
            UrgencyFactorMap.Add("asap", 1.5M);
        }

        public decimal CalculateAverageEstimate()
        {
            decimal SummedEstimate = 0.0M;

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
    }
}
