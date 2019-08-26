using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PluralSightBook.Core.Identity;
using PluralSightBook.Core.Models;
using PluralSightBook.Infrastructure.Data.ModelConfig;

namespace PluralSightBook.Infrastructure.Data
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