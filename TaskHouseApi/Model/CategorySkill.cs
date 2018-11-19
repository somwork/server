namespace TaskHouseApi.Model
{
    public class CategorySkill
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int SkillId { get; set; }
        public Skill Skill { get; set; }
    }
}
