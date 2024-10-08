using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication9.Entities
{
    public class DataBaseContext : IdentityDbContext<IdentityUser>
    {
        public DataBaseContext(DbContextOptions options)
            : base(options)
        {
        }
        public virtual DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Your code setting up foreign keys of whatever!
        }
    }
}
