using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Starter.Data.Entities;

namespace Starter.Data.Config
{
    class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.HasKey(m => m.Id)
                .ForSqlServerIsClustered(true);

            builder
                .HasOne(u => u.Identity)
                .WithOne(u => u.Profile)
                .HasForeignKey<UserProfile>(u => u.IdentityId);
        }
    }
}
