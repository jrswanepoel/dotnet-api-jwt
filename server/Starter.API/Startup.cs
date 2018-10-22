using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Starter.API.Config;

namespace Starter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddLogging()
                .AddSwaggerDocs()
                .AddCors();

            if (Environment.IsDevelopment())
            {
                var dbInMemory = !Convert.ToBoolean(Configuration["DevUseInMemoryDb"]);
                if (!dbInMemory)
                    services.AddDataSqlServerDb(Configuration);
                else
                    services.AddDataInMemoryDb();
            }
            else if (Environment.IsIntegrationTest())
                services.AddDataInMemoryDb();
            else if (Environment.IsProduction())
                services.AddDataSqlServerDb(Configuration);

            services.AddJwtAuthentication(Configuration);

        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                loggerFactory.UseDevLogs();

                app.UseCors(options =>
                    options
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseAuthentication();

            app.UseSwaggerDocs();
        }
    }
}