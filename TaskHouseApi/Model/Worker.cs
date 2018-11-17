using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Worker : User
    {
        public List<Offer> Offers { get; set; }
        public List<Reference> References { get; set; }
        public List<Education> Educations { get; set; }
        public List<Skill> Skills { get; set; }
        public Worker()
        {
        }
    }
}
