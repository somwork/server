namespace TaskHouseApi.Model
{
    public class Reference : BaseModel
    {
        public int Rating { get; set; }
        public string Statement { get; set; }

        public int WorkerId { get; set; }
        public int TaskId { get; set; }

        public Reference() { }
    }
}
