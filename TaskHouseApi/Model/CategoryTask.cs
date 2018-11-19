namespace TaskHouseApi.Model
{
    public class CategoryTask
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
