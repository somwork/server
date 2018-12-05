using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model

{
    public class Estimate : BaseModel
    {
        [Required]
        public int TotalHours { get; set; }
        [Required]
        public double Complexity { get; set; }
        [Required]
        public double HourlyWage { get; set; }
        public bool Accepted { get; set; } = false;
        [Required]
        public double Urgency { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public int TaskId { get; set; }

        public Estimate(){}

        public Estimate(int TotalHours, double Complexity, double HourlyWage, double Urgency)
        {
            this.TotalHours = TotalHours;
            this.Complexity = Complexity;
            this.HourlyWage = HourlyWage;
            this.Urgency = Urgency;
        }

        public double CalculateEstimate()
        {
            return TotalHours * HourlyWage * Complexity * Urgency;
        }
    }
}
