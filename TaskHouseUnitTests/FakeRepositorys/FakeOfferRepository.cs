using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositorys
{
    public class FakeOfferRepository : FakeRepository<Offer>, IOfferRepository
    {
        public FakeOfferRepository()
            : base(new List<Offer>()
                {
                    new Offer()
                    {
                        Id = 1,
                        Accepted = false,
                        Price = 213.2M,
                        Currency = "DKK",
                        WorkerId = 4,
                        TaskId = 1
                    },
                    new Offer()
                    {
                        Id = 2,
                        Accepted = true,
                        Price = 213.2M,
                        Currency = "DKK",
                        WorkerId = 5,
                        TaskId = 1
                    },
                    new Offer()
                    {
                        Id = 3,
                        Accepted = false,
                        Price = 213.2M,
                        Currency = "DKK",
                        WorkerId = 6,
                        TaskId = 1
                    }
                }
            )
        { }
    }
}
