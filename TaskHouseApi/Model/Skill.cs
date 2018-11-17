using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Skill : BaseModel
    {
        public string Title { get; set; }
        public int WorkerId { get; set; }
        public List<Category> Categorys { get; set; }

        public Skill()
        {
        }
    }
}
