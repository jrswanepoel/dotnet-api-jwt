using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Starter.API.Config
{
    public static class ConfigureServices
    {
        /// <summary>
        /// Sets up Swagger DI. To be used in Services
        /// </summary>
        public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
        {
            var swaggerTitle = Constants.Swagger.SwaggerTitle;
            var swaggerVersion = Constants.Swagger.SwaggerVersion;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerVersion, new Swashbuckle.AspNetCore.Swagger.Info
                { Title = swaggerTitle, Version = swaggerVersion });
                c.DescribeAllEnumsAsStrings();
            });

            return services;
        }

        /// <summary>
        /// Sets up Swagger Configuration. To be used in Configure method
        /// </summary>
        public static IApplicationBuilder UseSwaggerDocs(this IApplicationBuilder app)
        {
            var swaggerTitle = Constants.Swagger.SwaggerTitle;
            var swaggerVersion = Constants.Swagger.SwaggerVersion;
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{swaggerVersion}/swagger.json", $"{swaggerTitle} {swaggerVersion}");
                c.SupportedSubmitMethods(new SubmitMethod[] { });
            });

            return app;
        }

        /// <summary>
        /// Sets up Development and Testing Logs. To be used in Configure Method
        /// </summary>
        public static ILoggerFactory UseDevLogs(this ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole(LogLevel.Debug)
                .AddDebug(LogLevel.Debug);

            return loggerFactory;
        }
    }
}
