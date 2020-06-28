using Lazulisoft.ApiCleanArchtMediatR.Domain.Data;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories;
using Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Repositories
        private ICharacterRepository characterRepository;

        public ICharacterRepository CharacterRepository
        {
            get
            {
                if (characterRepository == null)
                {
                    characterRepository = new CharacterRepository(_dbContext);
                }
                return characterRepository;
            }
        }
        #endregion

        public virtual void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public virtual void RollbackTransaction()
        {
            _dbContext.Database.RollbackTransaction();
        }

        public virtual void CommitTransaction()
        {
            _dbContext.Database.CommitTransaction();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }
    }
}
