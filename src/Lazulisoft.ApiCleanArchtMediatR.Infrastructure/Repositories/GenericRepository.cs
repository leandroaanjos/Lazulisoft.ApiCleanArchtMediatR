using Lazulisoft.ApiCleanArchtMediatR.Domain.Entities;
using Lazulisoft.ApiCleanArchtMediatR.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lazulisoft.ApiCleanArchtMediatR.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext DbContext;

        public GenericRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DbContext.Set<TEntity>().ToListAsync();
        }

        public virtual IQueryable<TEntity> All()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Count(predicate) > 0;
        }

        public virtual void Add(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            DbContext.Set<TEntity>().Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }
    }
}