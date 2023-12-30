using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace LeHotelMax.Infrastructure.Interceptors
{
    internal static class AuditableEntityInterceptorExtensionHelpers
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry)
        {

            return entry.References.Any(r =>
                       r.TargetEntry != null &&
                       r.TargetEntry.Metadata.IsOwned() &&
                       (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
        }
    }
}
