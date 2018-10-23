using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

using Starter.Data.Context;
using Starter.API.Authentication;
using static Starter.API.Config.Constants.Jwt;

namespace Starter.API.Config
{
    public static class ConfigureAuthServices
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // Get options from app settings
            var jwtAppSettingOptions = config.GetSection(nameof(JwtIssuerOptions));

            var issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
            var audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];

            //todo: get secure secret key and store elsewhere
            string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";
            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = issuer;
                options.Audience = audience;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("BasicUser", policy =>
                    policy.RequireClaim(JwtClaimIdentifiers.Rol, JwtClaims.ApiAccess));
                options.AddPolicy("AdminUser", policy =>
                    policy.RequireClaim(JwtClaimIdentifiers.Rol, JwtClaims.AdminAccess));
                options.AddPolicy("SuperUser", policy =>
                    policy.RequireClaim(JwtClaimIdentifiers.Rol, JwtClaims.SuperAccess));
            });

            var builder = services.AddIdentityCore<IdentityUser>(o =>
            {
                // configure identity options
                o.User.RequireUniqueEmail = true;
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            var idBuilder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            idBuilder
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
