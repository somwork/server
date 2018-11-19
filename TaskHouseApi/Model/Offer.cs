namespace TaskHouseApi.Model
{
    public class Offer : BaseModel
    {
        public bool Accepted { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int WorkerId { get; set; }
        public int TaskId { get; set; }

        public Offer() { }
    }
}
