using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAll();

        IQueryable<TEntity> All();

        Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        bool Contains(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}