using System.Collections.Generic;

namespace TaskHouseApi.Model
{
    public class Worker : User
    {
        public List<Offer> Offers { get; set; }
        
        public Worker()
        {
        }
    }
}
