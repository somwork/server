
using Newtonsoft.Json;

namespace TaskHouseApi.Model

{
    public class Estimate : BaseModel
    {
        public int TotalHours { get; set; }

        public int Complexity { get; set; }

        public decimal HourlyWage { get; set; }
        public bool Accepted { get; set; } = false;
        public decimal Urgency { get; set; }
        public int WorkerId { get; set; }
        public int TaskId { get; set; }
        public decimal PriceEstimate { get; set; }

        public Estimate(int TotalHours, int Complexity, decimal HourlyWage, decimal Urgency)
        {
            this.TotalHours = TotalHours;
            this.Complexity = Complexity;
            this.HourlyWage = HourlyWage;
            this.Urgency = Urgency;

            this.PriceEstimate = Calculate();
        }

        public decimal Calculate()
        {
            return TotalHours * HourlyWage * Complexity * Urgency;
        }
    }
}
