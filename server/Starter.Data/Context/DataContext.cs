using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Starter.Data.Config;
using Starter.Data.Entities;

namespace Starter.Data.Context
{
    public class DataContext : IdentityDbContext<UserAccount>, IDataContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }

        public DataContext()
            : base()
        {

        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
