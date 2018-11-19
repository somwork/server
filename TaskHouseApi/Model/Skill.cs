using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Skill : BaseModel
    {
        public string Title { get; set; }
        public int WorkerId { get; set; }
        public virtual ICollection<CategorySkill> CategorySkill { get; set; }

        public Skill()
        {
            CategorySkill = new List<CategorySkill>();
        }
    }
}
