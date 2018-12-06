using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.DatabaseContext
{
    public static class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PostgresContext(
                serviceProvider.GetRequiredService<DbContextOptions<PostgresContext>>()))
            {
                // Look for any Users
                if (context.Users.Any())
                {
                    return;   // DB has been seeded
                }

                context.Workers.AddRange(
                    new Worker
                    {
                        Username = "root",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", //12345678
                        Email = "root@root.com",
                        FirstName = "Bob",
                        LastName = "Bobsen",
                        Salt = "pFxZH4br1PTUImBtIUGljQ=="
                    },
                    new Worker()
                    {
                        Username = "1234",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "test@test.com",
                        FirstName = "Bob1",
                        LastName = "Bobsen1",
                        Salt = "pFxZH4br1PTUImBtIUGljQ=="
                    },
                    new Worker()
                    {
                        Username = "hej",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "test@test.com",
                        FirstName = "Bob3",
                        LastName = "Bobsen3",
                        Salt = "pFxZH4br1PTUImBtIUGljQ=="
                    },
                    new Worker
                    {
                        Username = "w",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "root@root.com",
                        FirstName = "Bob",
                        LastName = "Bobsen",
                        Salt = "pFxZH4br1PTUImBtIUGljQ=="
                    }
                );
                context.SaveChanges();

                context.Employers.AddRange(
                    new Employer
                    {
                        Username = "e",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "root@root.com",
                        FirstName = "em1",
                        LastName = "emsen1",
                        Salt = "pFxZH4br1PTUImBtIUGljQ=="
                    },
                    new Employer()
                    {
                        Username = "em2",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "test@test.com",
                        FirstName = "em2",
                        LastName = "emsen2",
                        Salt = "pFxZH4br1PTUImBtIUGljQ==",
                    },
                    new Employer()
                    {
                        Username = "em3",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "test@test.com",
                        FirstName = "em3",
                        LastName = "emsen3",
                        Salt = "pFxZH4br1PTUImBtIUGljQ==",
                    }
                );
                context.SaveChanges();

                context.QualityAssurance.AddRange(
                    new QualityAssurance
                    {
                        Username = "q",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "root@root.com",
                        FirstName = "qa1",
                        LastName = "emsen1",
                        Salt = "pFxZH4br1PTUImBtIUGljQ=="
                    },
                    new QualityAssurance()
                    {
                        Username = "qa2",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "test@test.com",
                        FirstName = "qa2",
                        LastName = "emsen2",
                        Salt = "pFxZH4br1PTUImBtIUGljQ==",
                    },
                    new QualityAssurance()
                    {
                        Username = "qa3",
                        Password = "YFvty9NF4RMGZwJfMuBiL7lsMVebE1xsItAt9qFRI3w=", ////12345678
                        Email = "test@test.com",
                        FirstName = "qa3",
                        LastName = "emsen3",
                        Salt = "pFxZH4br1PTUImBtIUGljQ==",
                    }
                );
                context.SaveChanges();

                context.Budgets.AddRange(
                    new Budget()
                    {
                        From = 0.0M,
                        To = 500.0M,
                        Currency = "USD"
                    },
                    new Budget()
                    {
                        From = 501.0M,
                        To = 1000.0M,
                        Currency = "USD"
                    },
                    new Budget()
                    {
                        From = 1001.0M,
                        To = 10000.0M,
                        Currency = "USD"
                    },
                    new Budget()
                    {
                        From = 10001.0M,
                        To = 999999.0M,
                        Currency = "USD"
                    }
                );
                context.SaveChanges();

                context.Categories.AddRange(
                    new Category()
                    {
                        Title = "Software",
                        Description = "des"
                    },
                    new Category()
                    {
                        Title = "Cat2",
                        Description = "des"
                    },
                    new Category()
                    {
                        Title = "Cat3",
                        Description = "des"
                    },
                    new Category()
                    {
                        Title = "Cat4",
                        Description = "des"
                    },
                    new Category()
                    {
                        Title = "Cat5",
                        Description = "des"
                    },
                    new Category()
                    {
                        Title = "Cat6",
                        Description = "des"
                    },
                    new Category()
                    {
                        Title = "Cat7",
                        Description = "des"
                    }
                );
                context.SaveChanges();

                context.Locations.AddRange
                (
                    new Location()
                    {
                        Country = "Country1",
                        City = "City1",
                        ZipCode = "ZipCode1",
                        PrimaryLine = "PrimaryLine1",
                        SecondaryLine = "SecondaryLine1",
                        UserId = 1
                    },
                    new Location()
                    {
                        Country = "Country2",
                        City = "City2",
                        ZipCode = "ZipCode2",
                        PrimaryLine = "PrimaryLine2",
                        SecondaryLine = "SecondaryLine2",
                        UserId = 2
                    },
                    new Location()
                    {
                        Country = "Country3",
                        City = "City3",
                        ZipCode = "ZipCode3",
                        PrimaryLine = "PrimaryLine3",
                        SecondaryLine = "SecondaryLine3",
                        UserId = 3
                    }
                );
                context.SaveChanges();

                context.Skills.AddRange(
                    new Skill()
                    {
                        Title = "Skill1",
                        WorkerId = 1
                    },
                    new Skill()
                    {
                        Title = "Skill2",
                        WorkerId = 1
                    },
                    new Skill()
                    {
                        Title = "Skill3",
                        WorkerId = 2
                    },
                    new Skill()
                    {
                        Title = "Skill4",
                        WorkerId = 2
                    },
                    new Skill()
                    {
                        Title = "Skill5",
                        WorkerId = 2
                    },
                    new Skill()
                    {
                        Title = "Skill6",
                        WorkerId = 3
                    }
                );
                context.SaveChanges();

                context.Tasks.AddRange(
                    new Task()
                    {
                        Title = "task1",
                        Start = new DateTime(2018, 12, 5),
                        Deadline = new DateTime(2019, 2, 4),
                        Description = "task",
                        Urgency = 1.2D,
                        BudgetId = 1,
                        EmployerId = 5,
                        UrgencyString = "norush",
                        Completed = false

                    },
                    new Task()
                    {
                        Title = "task2",
                        Start = new DateTime(2018, 12, 5),
                        Deadline = new DateTime(2019, 2, 5),
                        Description = "task",
                        Urgency = 1.4D,
                        BudgetId = 2,
                        EmployerId = 6,
                        UrgencyString = "urgent",
                        Completed = false
                    },
                    new Task()
                    {
                        Title = "task3",
                        Start = new DateTime(2018, 1, 5),
                        Deadline = new DateTime(2019, 2, 5),
                        Description = "task",
                        Urgency = 1.5D,
                        BudgetId = 2,
                        EmployerId = 6,
                        UrgencyString = "asap",
                        Completed = false
                    },
                    new Task()
                    {
                        Title = "task4",
                        Start = new DateTime(2018, 11, 5),
                        Deadline = new DateTime(2018, 12, 5),
                        Description = "task",
                        Urgency = 1.5D,
                        BudgetId = 2,
                        EmployerId = 6,
                        UrgencyString = "asap",
                        Completed = true
                    }
                );
                context.SaveChanges();

                context.References.AddRange(
                    new Reference()
                    {
                        Rating = 5,
                        Statement = "Good",
                        WorkerId = 1,
                        TaskId = 4
                    }
                );
                context.SaveChanges();

                context.Messages.AddRange(
                    new Message()
                    {
                        Text = "m1Worker",
                        SendAt = new DateTime(2018, 12, 3, 16, 5, 0), //yyyyMMddHHmmss
                        UserId = 1,
                        FirstName = "Bob",
                        LastName = "Bobsen",
                        TaskId = 1
                    },
                    new Message()
                    {
                        Text = "m2Employer",
                        SendAt = new DateTime(2018, 12, 3, 16, 6, 0), //yyyyMMddHHmmss
                        UserId = 5,
                        FirstName = "em1",
                        LastName = "emsen1",
                        TaskId = 1
                    },
                    new Message()
                    {
                        Text = "m3Worker",
                        SendAt = new DateTime(2018, 12, 3, 19, 5, 0), //yyyyMMddHHmmss
                        UserId = 1,
                        FirstName = "Bob",
                        LastName = "Bobsen",
                        TaskId = 1
                    },
                    new Message()
                    {
                        Text = "m4Employer",
                        SendAt = new DateTime(2018, 12, 3, 20, 6, 0), //yyyyMMddHHmmss
                        UserId = 5,
                        FirstName = "em1",
                        LastName = "emsen1",
                        TaskId = 1
                    }
                );
                context.SaveChanges();

                context.Estimates.AddRange(
                    new Estimate()
                    {
                        TotalHours = 12,
                        Complexity = 1,
                        HourlyWage = 40,
                        Accepted = false,
                        Urgency = 1.2,
                        WorkerId = 1,
                        TaskId = 1
                    },
                    new Estimate()
                    {
                        TotalHours = 122,
                        Complexity = 2,
                        HourlyWage = 50,
                        Accepted = false,
                        Urgency = 1.2,
                        WorkerId = 2,
                        TaskId = 1
                    },
                    new Estimate()
                    {
                        TotalHours = 90,
                        Complexity = 2,
                        HourlyWage = 60,
                        Accepted = false,
                        Urgency = 1.2,
                        WorkerId = 3,
                        TaskId = 1
                    },
                    new Estimate()
                    {
                        TotalHours = 90,
                        Complexity = 2,
                        HourlyWage = 60,
                        Accepted = true,
                        Urgency = 1.4,
                        WorkerId = 4,
                        TaskId = 2
                    }
                );
                context.SaveChanges();

                context.CategorySkill.AddRange(
                    new CategorySkill()
                    {
                        CategoryId = 1,
                        SkillId = 1
                    },
                    new CategorySkill()
                    {
                        CategoryId = 1,
                        SkillId = 2
                    },
                    new CategorySkill()
                    {
                        CategoryId = 2,
                        SkillId = 3
                    },
                    new CategorySkill()
                    {
                        CategoryId = 4,
                        SkillId = 4
                    },
                    new CategorySkill()
                    {
                        CategoryId = 5,
                        SkillId = 5
                    },
                    new CategorySkill()
                    {
                        CategoryId = 6,
                        SkillId = 6
                    }
                );
                context.SaveChanges();

                context.CategoryTask.AddRange(
                    new CategoryTask()
                    {
                        CategoryId = 1,
                        TaskId = 1
                    },
                    new CategoryTask()
                    {
                        CategoryId = 2,
                        TaskId = 1
                    },
                    new CategoryTask()
                    {
                        CategoryId = 3,
                        TaskId = 2
                    },
                    new CategoryTask()
                    {
                        CategoryId = 4,
                        TaskId = 3
                    },
                    new CategoryTask()
                    {
                        CategoryId = 5,
                        TaskId = 4
                    }

                );
                context.SaveChanges();
            }
        }
    }
}
