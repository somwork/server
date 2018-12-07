using System;
using System.Collections.Generic;
using TaskHouseApi.Model;
using TaskHouseApi.Persistence.Repositories.Interfaces;

namespace TaskHouseUnitTests.FakeRepositories
{
    public class FakeCategoryRepository : FakeRepository<Category>, ICategoryRepository
    {
        public FakeCategoryRepository()
            : base(new List<Category>()
                {
                    new Category()
                    {
                        Id = 1,
                        Title = "edu1",
                        Description = "cate1"
                    },
                    new Category()
                    {
                        Id = 2,
                        Title = "edu2",
                        Description = "cate2"
                    },
                    new Category()
                    {
                        Id = 3,
                        Title = "edu3",
                        Description = "cate3"
                    }
                }
            )
        { }

        public IEnumerable<Category> GetCategoriesForTask(int taskId)
        {
            //NOT TESTABLE
            return null;
        }
    }
}
