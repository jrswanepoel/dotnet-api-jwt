using Microsoft.AspNetCore.Mvc;

namespace Starter.API.UnitTests.Controllers.Resources
{
    public static class Extensions
    {
        public static TValue GetValue<TResult, TValue>(this IActionResult result)
            where TResult : ObjectResult
            where TValue : class
        {
            if (result is TResult actionResult)
                return (TValue)actionResult.Value;
            return default(TValue);
        }
    }
}
