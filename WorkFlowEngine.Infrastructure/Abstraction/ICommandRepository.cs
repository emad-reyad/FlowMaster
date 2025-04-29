using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;
using WorkFlowEngine.Infrastructure.DataAccess;

namespace WorkFlowEngine.Infrastructure.Abstraction
{
    public interface IProcessMangerCommandReposistory : ICommandRepository<AppDbContext>
    {
    }
    public interface ICommandRepository<TDbContext>
    {
        Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
        void ClearChangeTracker();
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;
        Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
        IEnumerable<TEntity> Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        Task AddAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<int> ExecuteUpdateAsync<TEntity>(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default) where TEntity : class;
        Task<int> ExecuteDeleteAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class; Task<int> ExecuteDeleteAsync<TEntity>(
           Expression<Func<TEntity, bool>> condition,
           CancellationToken cancellationToken = default)
           where TEntity : class;
        void Attch<TEntity>(TEntity entity, bool isNoChangedState) where TEntity : class;

    }
}
