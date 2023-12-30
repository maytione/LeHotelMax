using LeHotelMax.Domain.Aggregates;
using LeHotelMax.Infrastructure.Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace LeHotelMax.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; }
        public DbSet<Hotel> Hotels { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
