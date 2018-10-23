using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHouseApi.Model
{

    public class Worker : User
    {
        public List<Education> Educations { get; set; }
        
        public Worker()
        {
        }
    }
}
