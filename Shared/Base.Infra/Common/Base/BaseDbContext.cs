using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.Infra.Common.Base
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Soft delete global filter
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!entityType.IsOwned() && typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var conn = FormattedConnectionString(GetType());
            optionsBuilder.UseNpgsql(conn);

            if (GetType().IsAssignableTo(typeof(ReadonlyDbContext)))
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
            else
            {
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public static string FormattedConnectionString(Type dbContextType)
        {
            if (RuntimeContext.Config.ConnectionStrings == null)
                throw new InvalidOperationException("Connection strings not configured.");

            var connectionString = RuntimeContext.Config.ConnectionStrings.ReadOnlyDbConnectionString;
            if (dbContextType.IsAssignableTo(typeof(PrimaryDbContext)))
            {
                connectionString = RuntimeContext.Config.ConnectionStrings.PrimaryDbConnectionString;
            }

            return string.Format(connectionString, RuntimeContext.TenantId);
        }
    }


}
