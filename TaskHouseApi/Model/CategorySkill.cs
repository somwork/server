using System.ComponentModel.DataAnnotations;

namespace TaskHouseApi.Model
{
    public class CategorySkill
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public int SkillId { get; set; }
        [Required]
        public Skill Skill { get; set; }
    }
}
