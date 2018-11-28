using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model

{
    public class Estimate : BaseModel
    {
        public int TotalHours { get; set; }

        public int Complexity { get; set; }

        public Currency Currency { get; set; }
        public decimal HourlyWage { get; set; }

        public decimal PriceEstimate { get; set; }
        public decimal AverageEstimate { get; set; }

        public decimal Urgency { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public Estimate(int TotalHours, int Complexity, Currency Currency, decimal HourlyWage, decimal Urgency)
        {
            this.TotalHours = TotalHours;
            this.Complexity = Complexity;
            this.Currency = Currency;
            this.HourlyWage = HourlyWage;
            this.Urgency = Urgency;

            Calculate();

        }


        public void Calculate()
        {
            this.PriceEstimate =
            HourlyWage * TotalHours * Urgency * Complexity;
        }

    }
}
