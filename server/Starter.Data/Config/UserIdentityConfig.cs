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
            builder.Entity<UserAccount>().ToTable("Users","ido");
            builder.Entity<IdentityRole>().ToTable("Roles", "ido");
            builder.Entity<IdentityUserRole<TKey>>().ToTable("UserRoles", "ido");
            builder.Entity<IdentityUserClaim<TKey>>().ToTable("UserClaims", "ido");
            builder.Entity<IdentityUserLogin<TKey>>().ToTable("UserLogins", "ido");
            builder.Entity<IdentityUserToken<TKey>>().ToTable("UserTokens", "ido");
            builder.Entity<IdentityRoleClaim<TKey>>().ToTable("RoleClaims", "ido");

            return builder;
        }
    }
}
