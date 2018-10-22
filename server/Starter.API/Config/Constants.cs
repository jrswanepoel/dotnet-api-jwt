
namespace Starter.API.Config
{
    public static partial class Constants
    {
        public static class Swagger
        {
            public const string SwaggerVersion = "v1";
            public const string SwaggerTitle = "Starter API";
        }

        public static class Database
        {
            public const string ContextSqlConnection = "StarterDb";
            public const string ContextInMemoryConnection = "Memory_StarterDb";
        }

        public static class Jwt
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }
            public const int DefaultBlockTime = (3600 * 1000); //1 hour in milliseconds

            public static class JwtClaims
            {
                public const string ApiAccess = "basic_access";
                public const string AdminAccess = "admin_access";
                public const string SuperAccess = "super_access";
            }
        }

    }
}
