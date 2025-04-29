using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Infrastructure.DataAccess;

namespace WorkFlowEngine.Infrastructure.Repositories
{
    public class ProcessMangerCommandReposistory : SqlCommandRepository<AppDbContext>, IProcessMangerCommandReposistory
    {
        public ProcessMangerCommandReposistory(AppDbContext context) : base(context)
        {
        }
    }
    public class SqlCommandRepository<TDbContext> : ICommandRepository<TDbContext> where TDbContext : Microsoft.EntityFrameworkCore.DbContext
    {

        private readonly TDbContext _dbContext;

        public SqlCommandRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public IEnumerable<TEntity> Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            _dbContext.SaveChanges();
            return entities;
        }

        public Task AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
            return _dbContext.SaveChangesAsync();
        }

        public Task AddAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            _dbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
            return _dbContext.SaveChangesAsync();
        }

        public void Attch<TEntity>(TEntity entity, bool isNoChangedState) where TEntity : class
        {
            var _entity = _dbContext.Attach<TEntity>(entity);
            if (isNoChangedState)
                _entity.State = EntityState.Modified;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Unspecified, CancellationToken cancellationToken = default)
        {
            Task<IDbContextTransaction> dbContextTransaction = _dbContext.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
            return dbContextTransaction;
        }

        public void ClearChangeTracker() => _dbContext.ChangeTracker.Clear();


        public Task<int> ExecuteDeleteAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
        {
            var count = _dbContext.Set<TEntity>().ExecuteDeleteAsync(cancellationToken);
            return count;
        }

        public Task<int> ExecuteDeleteAsync<TEntity>(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default) where TEntity : class
        {
            var count = _dbContext.Set<TEntity>().Where(condition).ExecuteDeleteAsync(cancellationToken);
            return count;
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql, parameters);

        }

        public Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return _dbContext.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }

        public Task<int> ExecuteUpdateAsync<TEntity>(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default) where TEntity : class
        {
            return _dbContext.Set<TEntity>().ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Remove<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            _dbContext.SaveChanges();

        }
    }
}
