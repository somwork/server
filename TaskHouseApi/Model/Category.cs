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
        public virtual ICollection<CategorySkill> CategorySkill { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryTask> CategoryTask { get; set; }

        public Category()
        {
            CategorySkill = new List<CategorySkill>();
            CategoryTask = new List<CategoryTask>();
        }
    }
}
