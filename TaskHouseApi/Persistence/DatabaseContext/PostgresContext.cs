using System;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Model;
using TaskHouseApi.Model.ServiceModel;

namespace TaskHouseApi.Persistence.DatabaseContext
{
    public class PostgresContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Education> Educations { set; get; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Estimate> Estimates { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<CategoryTask> CategoryTask { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<QualityAssurance> QualityAssurance { get; set; }

        public PostgresContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryTask>().HasKey(sc => new { sc.CategoryId, sc.TaskId });

            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
