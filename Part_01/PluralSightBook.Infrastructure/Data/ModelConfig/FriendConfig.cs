using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PluralSightBook.Core.Models;

namespace PluralSightBook.Infrastructure.Data.ModelConfig
{
    public class FriendConfig : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.IdentityUser)
                .WithMany(u => u.Friends)
                .HasForeignKey(f => f.PluralSightBookIdentityUser);
        }
    }
}