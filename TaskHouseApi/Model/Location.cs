namespace TaskHouseApi.Model
{
    public class Location : BaseModel
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PrimaryLine { get; set; }
        public string SecondaryLine { get; set; }
        public int UserId { get; set; }

        public Location()
        {

        }
    }
}

