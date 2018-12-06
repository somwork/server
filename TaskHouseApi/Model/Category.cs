using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Category : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [JsonIgnore]
        public virtual ICollection<Skill> Skills { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryTask> CategoryTask { get; set; }

        public Category()
        {
            Skills = new List<Skill>();
            CategoryTask = new List<CategoryTask>();
        }
    }
}
