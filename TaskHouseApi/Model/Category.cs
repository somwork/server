using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Category : BaseModel
    {
        public string Title { get; set; }
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
