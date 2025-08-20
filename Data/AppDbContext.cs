using Microsoft.EntityFrameworkCore;
using TinderSwipe.Models;

namespace TinderSwipe.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
