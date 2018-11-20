using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class Category : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<CategorySkill> CategorySkill { get; set; }
        public virtual ICollection<CategoryTask> CategoryTask { get; set; }

        public Category()
        {
            CategorySkill = new List<CategorySkill>();
            CategoryTask = new List<CategoryTask>();
        }
    }
}
