using Microsoft.AspNetCore.Builder;

namespace Eventures.Middlewares
{
    public static class CusotmMiddlewareExtensions
    {
        public static IApplicationBuilder UseSeedDataMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedDataMiddleware>();
        }
    }
}
