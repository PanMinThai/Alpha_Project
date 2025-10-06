using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Base.Domain.BaseClass;
using Shared.Base.Infra.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public abstract class BaseRepository2<TEntity> : IBaseRepository<TEntity>
        where TEntity : IdEntity<Guid>
    {
        protected readonly PrimaryDbContext _writeContext;
        protected readonly ReadonlyDbContext _readContext;
        protected readonly DbSet<TEntity> _writeSet;
        protected readonly DbSet<TEntity> _readSet;

        protected BaseRepository2(PrimaryDbContext writeContext, ReadonlyDbContext readContext)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));

            _writeSet = _writeContext.Set<TEntity>();
            _readSet = _readContext.Set<TEntity>();
        }

        #region CRUD

        public virtual async Task AddAsync(TEntity entity)
        {
            await _writeSet.AddAsync(entity);   
            await _writeContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _writeSet.Update(entity);
            await _writeContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _writeSet.FindAsync([id]);
            if (entity == null)
                throw new InvalidOperationException($"Entity with id {id} not found.");

            _writeSet.Remove(entity);
            await _writeContext.SaveChangesAsync();
        }

        #endregion

        #region Query

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _readSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _readSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        #endregion
    }
}
