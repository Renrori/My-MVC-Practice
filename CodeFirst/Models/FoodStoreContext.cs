using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models
{
    public class FoodStoreContext : DbContext
    {
        public FoodStoreContext() { }

        public FoodStoreContext(DbContextOptions<FoodStoreContext> options) : base(options) { }
        
        public DbSet<Food> Foods { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
