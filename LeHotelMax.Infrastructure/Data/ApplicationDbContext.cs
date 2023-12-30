using LeHotelMax.Domain.Aggregates;
using LeHotelMax.Infrastructure.Data.Identity;
using LeHotelMax.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LeHotelMax.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions options) :  IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers => base.Set<ApplicationUser>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>(hotel =>
            {
                hotel.ToTable("Hotels"); 
                hotel.HasKey(h => h.Id);
                hotel.Property(h => h.Name).IsRequired();
                hotel.HasIndex(h => h.Name).IsUnique(); // Will not work with InMemory database provider !!! :(
                hotel.OwnsOne(h => h.GeoLocation, geo =>
                {
                    geo.Property(g => g.Latitude).IsRequired();
                    geo.Property(g => g.Longitude).IsRequired();
                });
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
