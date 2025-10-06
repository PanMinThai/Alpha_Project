using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Base.Domain.BaseClass;
using Shared.Base.Domain.Interface;
using Shared.Base.Infra.Common.Config;
using Shared.Base.Infra.Extension;
using Shared.Base.Share.Interface;
using System.Reflection;

namespace Shared.Base.Infra.Common.Base
{
    public abstract class BaseDbContext : DbContext
    {
        private readonly ContextScanSettings _contextScanSettings;

        protected BaseDbContext(DbContextOptions options, ContextScanSettings? contextScanSettings = null)
            : base(options)
        {
            _contextScanSettings = contextScanSettings ?? new ContextScanSettings();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var asm in _contextScanSettings.Assemblies)
                modelBuilder.ApplyConfigurationsFromAssembly(asm);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ApplyAuditAndSoftDeleteTracking();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditAndSoftDeleteTracking();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditAndSoftDeleteTracking()
        {
            var now = DateTimeOffset.UtcNow;
            var userId = Guid.Empty; 

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is ICreationTracking creationTrack)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (creationTrack.CreatedAt == default)
                            creationTrack.CreatedAt = now;
                        if (creationTrack.CreatedBy == Guid.Empty)
                            creationTrack.CreatedBy = userId;
                    }
                }

                if (entry.Entity is IUpdateTracking updateTrack && entry.Entity is IOptimisticLock optimisticLock)
                {
                    if (entry.State == EntityState.Modified)
                    {
                        updateTrack.UpdatedAt = now;
                        updateTrack.UpdatedBy = userId;
                        optimisticLock.UpdateToken = Guid.NewGuid();
                    }
                }

                if (entry.Entity is ISoftDeletable softDeletable && entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    softDeletable.IsDeleted = true;

                    if (entry.Entity is IUpdateTracking softTrack && entry.Entity is IOptimisticLock softLock)
                    {
                        softTrack.UpdatedAt = now;
                        softTrack.UpdatedBy = userId;
                        softLock.UpdateToken = Guid.NewGuid();
                    }
                }
            }
        }
    }

}
