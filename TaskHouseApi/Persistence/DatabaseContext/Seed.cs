using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Model;

namespace TaskHouseApi.Persistence.DatabaseContext
{
    public static class Seed
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasData
            (
                new Worker
                {
                    Id = -1,
                    Username = "root",
                    Password = "mxurWhuDuXFA6EMY11qsixSbftITzPbpOtBU+Kbdr6Q=", //root
                    Email = "root@root.com",
                    FirstName = "Bob",
                    LastName = "Bobsen",
                    Salt = "HplteyrRxcNz6bOoiZi4Qw=="
                },
                new Worker()
                {
                    Id = -2,
                    Username = "1234",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "Bob1",
                    LastName = "Bobsen1",
                    Salt = "upYKQSsrlub5JAID61/6pA=="
                },
                new Worker()
                {
                    Id = -3,
                    Username = "hej",
                    Password = "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", //hej
                    Email = "test@test.com",
                    FirstName = "Bob3",
                    LastName = "Bobsen3",
                    Salt = "U+cUJhQU56X+OCiGF9hb1g=="
                }
            );
            modelBuilder.Entity<Employer>().HasData
            (
                new Employer
                {
                    Id = -4,
                    Username = "em1",
                    Password = "mxurWhuDuXFA6EMY11qsixSbftITzPbpOtBU+Kbdr6Q=", //root
                    Email = "root@root.com",
                    FirstName = "em1",
                    LastName = "emsen1",
                    Salt = "HplteyrRxcNz6bOoiZi4Qw=="
                },
                new Employer()
                {
                    Id = -5,
                    Username = "em2",
                    Password = "+z490sXHo5u0qsSaxbBqEk9KsJtGqNhD8I8mVBdDJls=", //1234
                    Email = "test@test.com",
                    FirstName = "em2",
                    LastName = "emsen2",
                    Salt = "upYKQSsrlub5JAID61/6pA==",
                },
                new Employer()
                {
                    Id = -6,
                    Username = "em3",
                    Password = "dpvq1pIWkY9SudflCKrW6tqCItErcBljM1GhNPWlUmg=", //hej
                    Email = "test@test.com",
                    FirstName = "em3",
                    LastName = "emsen3",
                    Salt = "U+cUJhQU56X+OCiGF9hb1g==",
                }
            );
            modelBuilder.Entity<Location>().HasData
            (
                new Location()
                {
                    Id = -1,
                    Country = "Country1",
                    City = "City1",
                    ZipCode = "ZipCode1",
                    PrimaryLine = "PrimaryLine1",
                    SecondaryLine = "SecondaryLine1",
                },
                new Location()
                {
                    Id = -2,
                    Country = "Country2",
                    City = "City2",
                    ZipCode = "ZipCode2",
                    PrimaryLine = "PrimaryLine2",
                    SecondaryLine = "SecondaryLine2",
                },
                new Location()
                {
                    Id = -3,
                    Country = "Country3",
                    City = "City3",
                    ZipCode = "ZipCode3",
                    PrimaryLine = "PrimaryLine3",
                    SecondaryLine = "SecondaryLine3",
                }
            );
            modelBuilder.Entity<Skill>().HasData
            (
                new Skill()
                {
                    Id = -1,
                    Title = "Skill1"
                },
                new Skill()
                {
                    Id = -2,
                    Title = "Skill2"
                },
                new Skill()
                {
                    Id = -3,
                    Title = "Skill3"
                }
            );
            modelBuilder.Entity<Task>().HasData
            (
                new Task()
                {
                    Id = -1,
                    Description = "Task1",
                    EmployerId = -4
                },
                new Task()
                {
                    Id = -2,
                    Description = "Task2",
                    EmployerId = -4
                },
                new Task()
                {
                    Id = -3,
                    Description = "Task3",
                    EmployerId = -5
                },
                new Task()
                {
                    Id = -4,
                    Description = "Task4",
                    EmployerId = -4
                }
            );
        }
    }
}
