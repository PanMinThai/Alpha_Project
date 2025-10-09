using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Shared.Base.API.Interface;
using Shared.Base.Domain.DomainEvent;
using Shared.Base.Domain.Interface;
using Shared.Base.Infra.Common.Base;
using Shared.Base.Share.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Base.API.BaseClass
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrimaryDbContext _dbContext;
        private readonly IDomainEventProcessor _domainEventProcessor;
        private readonly ILogger<UnitOfWork> _logger;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(
            PrimaryDbContext dbContext,
            IDomainEventProcessor domainEventProcessor,
            ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _domainEventProcessor = domainEventProcessor ?? throw new ArgumentNullException(nameof(domainEventProcessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                return;

            _transaction = await _dbContext.Database.BeginTransactionAsync();
            _logger.LogInformation("Transaction started.");
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("No active transaction to commit.");

            try
            {
                // 🔹 Lấy domain events từ các entity đã thay đổi
                var domainEvents = CollectDomainEvents().ToList();

                // 🔹 Lưu database lần 1 (để có ID cho entity nếu cần)
                await _dbContext.SaveChangesAsync();

                // 🔹 Xử lý tất cả domain events đang chờ
                var allDomainEvents = new List<IEvent>();
                while (domainEvents.Count > 0)
                {
                    allDomainEvents.AddRange(domainEvents);
                    await _domainEventProcessor.TriggerExecutingDomainEvents(domainEvents);
                    domainEvents = CollectDomainEvents().ToList();
                    await _dbContext.SaveChangesAsync();
                }

                // 🔹 Commit transaction
                await _transaction.CommitAsync();
                _logger.LogInformation("Transaction committed successfully.");

                // 🔹 Gửi sự kiện sau khi commit
                await _domainEventProcessor.TriggerExecutedDomainEvents(allDomainEvents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during transaction commit. Rolling back...");
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                return;

            try
            {
                await _transaction.RollbackAsync();
                _logger.LogWarning("Transaction rolled back.");
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
                _logger.LogInformation("Transaction disposed.");
            }
        }

        private IEnumerable<IEvent> CollectDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<IDomainEventEntity>()
                .Where(x => x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var entity in domainEntities)
                entity.Entity.ClearDomainEvents();

            return domainEvents;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
    }
}
