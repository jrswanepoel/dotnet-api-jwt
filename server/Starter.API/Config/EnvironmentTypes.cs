using Microsoft.AspNetCore.Hosting;

namespace Starter.API.Config
{
    public static class EnvironmentTypes
    {
        public const string IntegrationTest = "IntegrationTesting";

        public static bool IsIntegrationTest(this IHostingEnvironment env)
        {
            return (env.EnvironmentName == IntegrationTest);
        }
    }
}
