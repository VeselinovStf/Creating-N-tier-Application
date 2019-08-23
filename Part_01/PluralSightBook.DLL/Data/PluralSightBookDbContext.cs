using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PluralSightBook.DLL.Data.ModelConfig;
using PluralSightBook.DLL.Data.Models;
using PluralSightBook.DLL.Identity;

namespace PluralSightBook.DLL.Data
{
    public class PluralSightBookDbContext : IdentityDbContext<PluralSightBookIdentityUser>
    {
        public DbSet<Friend> Friends { get; set; }

        public PluralSightBookDbContext(DbContextOptions<PluralSightBookDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration<Friend>(new FriendConfig());

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}