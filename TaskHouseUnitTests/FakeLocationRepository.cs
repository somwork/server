using TaskHouseApi.Model;
using TaskHouseApi.Controllers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests
{
    public class FakeLocationRepository : FakeRepository<Location>, ILocationRepository
    {
        public FakeLocationRepository()
            : base(new List<Location>()
                {
                    new Location()
                    {
                        Id = 1,
                        Country = "Country1",
                        City = "City1",
                        ZipCode = "ZipCode1",
                        PrimaryLine = "PrimaryLine1",
                        SecondaryLine = "SecondaryLine1",
                    },
                    new Location()
                    {
                        Id = 2,
                        Country = "Country2",
                        City = "City2",
                        ZipCode = "ZipCode2",
                        PrimaryLine = "PrimaryLine2",
                        SecondaryLine = "SecondaryLine2",
                    },
                    new Location()
                    {
                        Id = 3,
                        Country = "Country3",
                        City = "City3",
                        ZipCode = "ZipCode3",
                        PrimaryLine = "PrimaryLine3",
                        SecondaryLine = "SecondaryLine3",
                    },
                }
            )
        { }
    }
}
