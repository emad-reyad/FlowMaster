using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WorkFlowEngine.Infrastructure.DataAccess;

namespace WorkFlowEngine.Infrastructure.Abstraction
{
    public interface IProcessMangerQueryRepository : IQueryRepository<AppDbContext>
    {
    }
    public interface IQueryRepository<TDbContext>
    {
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
            where TEntity : class;
        Task<List<TEntity>> GetListAsync<TEntity>(
            Expression<Func<TEntity, bool>> condition,
            bool asNoTracking,
            CancellationToken cancellationToken = default)
            where TEntity : class;

        public Task<List<TResult>> GetSelectedPropertiesListAsync<TEntity, TResult>(
       Expression<Func<TEntity, bool>> condition,
        Expression<Func<TEntity, TResult>> selector,
       Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
       bool asNoTracking)
       where TEntity : class;


        Task<List<TEntity>> GetListAsync<TEntity>(
                     Expression<Func<TEntity, bool>> condition,
                     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
                     bool asNoTracking = false,
                     CancellationToken cancellationToken = default)
                     where TEntity : class;
        Task<List<TProjectedType>> GetListAsync<TEntity, TProjectedType>(
                Expression<Func<TEntity, TProjectedType>> selectExpression,
                CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<List<TProjectedType>> GetListAsync<TEntity, TProjectedType>(
                Expression<Func<TEntity, bool>> condition,
                Expression<Func<TEntity, TProjectedType>> selectExpression,
                CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> condition)
                where TEntity : class;
        Task<TEntity> GetAsync<TEntity>(
                Expression<Func<TEntity, bool>> condition,
                bool asNoTracking)
                where TEntity : class;
        Task<TEntity> GetAsync<TEntity>(
                Expression<Func<TEntity, bool>> condition,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
                bool asNoTracking)
                where TEntity : class;
        Task<TEntity> GetAsync<TEntity>(
              Expression<Func<TEntity, bool>> condition,
              Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
              Expression<Func<TEntity, object>> orderBy,
              bool orderByDescending,
              bool asNoTracking)
              where TEntity : class;
        Task<bool> ExistsAsync<TEntity>(CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<int> GetCountAsync<TEntity>(CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<int> GetCountAsync<TEntity>(IEnumerable<Expression<Func<TEntity, bool>>> conditions, CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<long> GetLongCountAsync<TEntity>(CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<long> GetLongCountAsync<TEntity>(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
                where TEntity : class;
        Task<long> GetLongCountAsync<TEntity>(IEnumerable<Expression<Func<TEntity, bool>>> conditions, CancellationToken cancellationToken = default)
                where TEntity : class;
    }
}

