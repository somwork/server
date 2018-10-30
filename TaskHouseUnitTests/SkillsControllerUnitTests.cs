using System;
using Xunit;
using TaskHouseApi.Controllers;
using TaskHouseApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using TaskHouseApi.Model;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TaskHouseUnitTests 
{ 
    public class SkillsControllerUnitTests 
    { 
        SkillsController controller; 
        ISkillRepository repo; 

        public SkillsControllerUnitTests() 
        { 
            repo = new FakeSkillRepository();
            controller = new SkillsController(repo);
        }

        [Fact]
        public async void  SkillsController_ReturnsAllElementsInRepo() 
        { 
            IEnumerable<Skill> result = await controller.Get();
            Assert.Equal(3, result.Count());

        }

        [Fact]
        public async void SkillsController_ReturnsObjectReponse_WhenGivenValidId() 
        { 
            int skillId = 2;

            var result = await controller.Get(skillId) as ObjectResult;
            

            AssemblyLoadEventArgs.Equals(200, result.StatusCode);


            // What to do ift. at teste returneringer a requests/actionsresults? 
        }
    }
}