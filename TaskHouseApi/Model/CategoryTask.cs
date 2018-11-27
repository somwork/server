using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class CategoryTask
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public int TaskId { get; set; }
        [Required]
        public Task Task { get; set; }
    }
}
