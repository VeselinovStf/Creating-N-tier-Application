using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PluralSightBook.DLL.Data.Models;

namespace PluralSightBook.DLL.Data.ModelConfig
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