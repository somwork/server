using System;
using Microsoft.EntityFrameworkCore;
using TaskHouseApi.Model;

namespace TaskHouseApi.DatabaseContext
{
    public class PostgresContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Location> Locations { get; set; }
        
        public PostgresContext()
        {
            
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=root;Username=root;Password=root;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
