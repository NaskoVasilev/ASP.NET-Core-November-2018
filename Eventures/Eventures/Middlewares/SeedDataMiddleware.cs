using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Eventures.Utilites;

namespace Eventures.Middlewares
{
    public class SeedDataMiddleware
    {
        private readonly RequestDelegate next;

        public SeedDataMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, Seeder seeder)
        {
            seeder.SeedNeededRoles(new string[] { "Administrator", "User" });
            seeder.SeedAdminUser();
            await next(httpContext);
        }
    }
}
