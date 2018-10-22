using System.IO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
