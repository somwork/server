using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Worker : User
    {
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Reference> References { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public Worker()
        {
            Offers = new List<Offer>();
            References = new List<Reference>();
            Educations = new List<Education>();
            Skills = new List<Skill>();
        }
    }
}
