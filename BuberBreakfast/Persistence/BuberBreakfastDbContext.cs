using BuberBreakfast.Models;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Persistence
{
    public class BuberBreakfastDbContext : DbContext
    {
        public BuberBreakfastDbContext(DbContextOptions<BuberBreakfastDbContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Breakfast> Breakfasts { get; set; } = null;

        // Use to define primary keys for: one to many relationships of classes, IE: Class { List<T> }
        /* Simple example 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Breakfast>()
        }
        */

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
