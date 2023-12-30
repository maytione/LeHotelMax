using LeHotelMax.Domain.Aggregates.ValueObjects;
using LeHotelMax.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace LeHotelMax.Infrastructure.Data
{
    internal class ApplicationDbContextInit
    {
        private readonly ILogger<ApplicationDbContextInit> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbContextInit(ILogger<ApplicationDbContextInit> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task InitDatabase()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while init database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await Seed();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while seeding!");
                throw;
            }
        }

        private async Task Seed()
        {
            var admin = new ApplicationUser { UserName = "admin@localhost", Email = "admin@localhost" };

            if (_userManager.Users.All(u => u.UserName != admin.UserName))
            {
                await _userManager.CreateAsync(admin, "Admin123!");
            }



            if (!_context.Hotels.Any())
            {
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Esplanade Zagreb Hotel", Price = 150, GeoLocation = new GeoLocation(45.8105, 15.9762) });
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Hotel Dubrovnik", Price = 127.99m, GeoLocation = new GeoLocation(45.8138, 15.9765) });
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Sheraton Zagreb Hotel", Price = 163.25m, GeoLocation = new GeoLocation(45.8088, 15.9782) });
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Falkensteiner Hotel & Spa Zadar", Price = 200, GeoLocation = new GeoLocation(44.1992, 15.2103) });
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Hotel Academia", Price = 99.99m, GeoLocation = new GeoLocation(45.8161, 15.9798) });
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Hotel Osijek", Price = 121m, GeoLocation = new GeoLocation(45.5530, 18.6972) });
                _context.Hotels.Add(new Domain.Aggregates.Hotel() { Name = "Hotel Jägerhorn", Price = 80.50m, GeoLocation = new GeoLocation(45.8140, 15.9780) });
                
                await _context.SaveChangesAsync();
            }
        }
    }
}
