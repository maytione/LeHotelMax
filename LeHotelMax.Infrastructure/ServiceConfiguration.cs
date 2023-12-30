using LeHotelMax.Application.Hotels.Interfaces;
using LeHotelMax.Application.Users.Interfaces;

using LeHotelMax.Infrastructure.Data;
using LeHotelMax.Infrastructure.Data.Identity;
using LeHotelMax.Infrastructure.Data.Identity.Services;
using LeHotelMax.Infrastructure.Data.Repository;
using LeHotelMax.Infrastructure.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceConfiguration
{
    /// <summary>
    /// AddInfrastructureServices - Setup services
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // register IApplicationDbContext interface
        services.AddScoped<IApplicationDbContext>(provider => provider
            .GetRequiredService<ApplicationDbContext>());

        // Register Seed database class
        services.AddScoped<ApplicationDbContextInit>();

        // register IHotelRepository interface (for Application layer usage)
        services.AddScoped<IHotelRepository, HotelRepository>();

        // register IIdentityRepository interface (for User login only - for test)
        services.AddScoped<IIdentityRepository, IdentityRepository>();

        // Configure JWT settings - Nothing smart, just fetch from configuration
        services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

        // Register JWT required interfaces
        services.AddScoped<IAccessTokenService, AccessTokenService>();
        services.AddScoped<IAuthenticateService, AuthenticateService>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();

        // add DbContext - for this example we are using in memory database
        // for real MSSQL please use:
        // .UseSqlServer(configuration.GetConnectionString("Default"))
        // instead
        // .UseInMemoryDatabase(configuration.GetConnectionString("Default")!)
        services.AddDbContext<ApplicationDbContext>((sp, options) => options
            .UseInMemoryDatabase(configuration.GetConnectionString("Default")!)
            .AddInterceptors(sp.GetServices<ISaveChangesInterceptor>()));

        // Setup Core Identity User with default IdenittyRole
        // Entity framework stores (user store, role store)
        // Setup default token providers (EmailTokenProvider, PhoneNumberTokenProvider,...)
        // Add default api endpoints for identity user
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders()
        .AddApiEndpoints();

        // for monitorig SQL database - if requred
        services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        return services;
    }
}
