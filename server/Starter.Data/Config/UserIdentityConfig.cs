using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Starter.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Data.Config
{
    public static class UserIdentityConfig
    {
        public static ModelBuilder ApplyIdentityConfiguration<TKey>(this ModelBuilder builder)
            where TKey : IEquatable<TKey>
        {
            builder.Entity<UserAccount>(options => options.ToTable("Users","ido"));
            builder.Entity<IdentityRole>(options => options.ToTable("Roles", "ido"));
            builder.Entity<IdentityUserRole<TKey>>(options => options.ToTable("UserRoles", "ido"));
            builder.Entity<IdentityUserClaim<TKey>>(options => options.ToTable("UserClaims", "ido"));
            builder.Entity<IdentityUserLogin<TKey>>(options => options.ToTable("UserLogins", "ido"));
            builder.Entity<IdentityUserToken<TKey>>(options => options.ToTable("UserTokens", "ido"));
            builder.Entity<IdentityRoleClaim<TKey>>(options => options.ToTable("RoleClaims", "ido"));

            return builder;
        }
    }
}
