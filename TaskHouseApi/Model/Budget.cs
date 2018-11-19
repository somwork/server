namespace TaskHouseApi.Model
{
    public class Budget : BaseModel
    {
        public decimal From { get; set; }
        public decimal To { get; set; }
        public string Currency { get; set; }

        public Budget()
        {
        }
    }
}
