using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace LeHotelMax.Infrastructure.Data
{
    public static class SeedInitExtension
    {
        public static async Task Seed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInit>();

            //Uncomment in case of real database
            //await initialiser.InitDatabase();

            await initialiser.SeedAsync();
        }
    }
}
