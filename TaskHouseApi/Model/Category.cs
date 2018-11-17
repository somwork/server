using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Category : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Task> Tasks { get; set; }

        public Category()
        {

        }
    }
}
