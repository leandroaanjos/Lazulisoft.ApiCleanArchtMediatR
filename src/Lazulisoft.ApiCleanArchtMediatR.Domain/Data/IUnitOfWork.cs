using Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Domain.Data
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories
        ICharacterRepository CharacterRepository { get; }

        #endregion

        void BeginTransaction();
        void RollbackTransaction();
        void CommitTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
