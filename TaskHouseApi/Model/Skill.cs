using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class Skill : BaseModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int WorkerId { get; set; }
        public virtual ICollection<CategorySkill> CategorySkill { get; set; }

        public Skill()
        {
            CategorySkill = new List<CategorySkill>();
        }
    }
}
