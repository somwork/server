namespace TaskHouseApi.Model
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public string Currency { get; set; }

        public Budget()
        {
        }
    }
}
