using System;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Model;

namespace TaskHouseApi.DatabaseContext
{
    public class PostgresContext : DbContext
    {
         public DbSet<User> Users { set; get; }
        public DbSet<Worker> Workers { set; get; }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Education> Educations { set; get; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public PostgresContext(DbContextOptions options)
            : base(options) { }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "root", Password = "mxurWhuDuXFA6EMY11qsixSbftITzPbpOtBU+Kbdr6Q=", Email = "root@root.com", FirstName = "Bob", LastName = "Bobsen", Salt = "HplteyrRxcNz6bOoiZi4Qw==" });
        }*/
    }

}
