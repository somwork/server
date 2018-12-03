using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model

{
    public class Estimate : BaseModel
    {
        [Required]
        public int TotalHours { get; set; }
        [Required]
        public int Complexity { get; set; }
        [Required]
        public decimal HourlyWage { get; set; }
        public bool Accepted { get; set; } = false;
        [Required]
        public decimal Urgency { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int TaskId { get; set; }

        public Estimate(){}

        public Estimate(int TotalHours, int Complexity, decimal HourlyWage, decimal Urgency)
        {
            this.TotalHours = TotalHours;
            this.Complexity = Complexity;
            this.HourlyWage = HourlyWage;
            this.Urgency = Urgency;
        }

        public decimal CalculateEstimate()
        {
            return TotalHours * HourlyWage * Complexity * Urgency;
        }
    }
}
