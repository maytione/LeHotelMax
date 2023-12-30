using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using LeHotelMax.Domain.Common;
using LeHotelMax.Domain.Interfaces;

namespace LeHotelMax.Infrastructure.Interceptors
{
    public class AuditableEntityInterceptor(IUser user, TimeProvider dateTime) : SaveChangesInterceptor
    {
        private readonly IUser _user = user;
        private readonly TimeProvider _dateTime = dateTime;


        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AddAuditables(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            AddAuditables(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddAuditables(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<BaseAuditable>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _user?.Id?.ToString();
                    entry.Entity.Created = _dateTime.GetUtcNow();
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.UpdatedBy = _user?.Id?.ToString();
                    entry.Entity.Updated = _dateTime.GetUtcNow();
                }
            }
        }
    }
}
