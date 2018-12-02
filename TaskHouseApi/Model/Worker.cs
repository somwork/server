using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaskHouseApi.Model
{
    public class Worker : User
    {
        [JsonIgnore]
        public virtual ICollection<Estimate> Estimates { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reference> References { get; set; }
        [JsonIgnore]
        public virtual ICollection<Education> Educations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Skill> Skills { get; set; }

        public Worker()
        {
            Estimates = new List<Estimate>();
            References = new List<Reference>();
            Educations = new List<Education>();
            Skills = new List<Skill>();
        }
    }
}
