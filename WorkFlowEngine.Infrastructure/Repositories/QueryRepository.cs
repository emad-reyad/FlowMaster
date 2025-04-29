using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Infrastructure.DataAccess;

namespace WorkFlowEngine.Infrastructure.Repositories
{

    public class QueryRepository<TDbContext> : IQueryRepository<TDbContext>
  where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public QueryRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetQueryable<T>() where T : class => _dbContext.Set<T>();


        public Task<List<T>> GetListAsync<T>(CancellationToken cancellationToken = default)
            where T : class => GetListAsync<T>(false, cancellationToken);

        public Task<List<T>> GetListAsync<T>(bool asNoTracking, CancellationToken cancellationToken = default)
            where T : class
        {
            Func<IQueryable<T>, IIncludableQueryable<T, object>> nullValue = null;
            return GetListAsync(nullValue, asNoTracking, cancellationToken);
        }

        public Task<List<T>> GetListAsync<T>(
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            CancellationToken cancellationToken = default)
            where T : class => GetListAsync(includes, false, cancellationToken);

        public async Task<List<T>> GetListAsync<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default)
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
                query = includes(query);
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.AsSplitQuery().ToListAsync(cancellationToken);
        }

        public Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
             where T : class => GetListAsync(condition, false, cancellationToken);


        public Task<List<T>> GetListAsync<T>(
            Expression<Func<T, bool>> condition,
            bool asNoTracking,
            CancellationToken cancellationToken = default)
             where T : class => GetListAsync(condition, null, asNoTracking, cancellationToken);



        public async Task<List<T>> GetListAsync<T>(
            Expression<Func<T, bool>> condition,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default)
             where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
                query = query.Where(condition);
            if (includes != null)
                query = includes(query);
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.AsSplitQuery().ToListAsync(cancellationToken);
        }


        public async Task<List<TProjectedType>> GetListAsync<T, TProjectedType>(
            Expression<Func<T, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default)
            where T : class
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            List<TProjectedType> entities = await _dbContext.Set<T>()
                .Select(selectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

            return entities;
        }

        public async Task<List<TProjectedType>> GetListAsync<T, TProjectedType>(
            Expression<Func<T, bool>> condition,
            Expression<Func<T, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default)
            where T : class
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            List<TProjectedType> projectedEntites = await query.Select(selectExpression)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return projectedEntites;
        }


        public async Task<T> GetAsync<T>(
            Expression<Func<T, bool>> condition)
           where T : class => await GetAsync(condition, null, false);


        public async Task<T> GetAsync<T>(
            Expression<Func<T, bool>> condition,
            bool asNoTracking)
           where T : class => await GetAsync(condition, null, asNoTracking);


        public async Task<T> GetAsync<T>(
            Expression<Func<T, bool>> condition,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            bool asNoTracking)
           where T : class => await GetAsync(condition, includes, null, false, false);

        public async Task<T> GetAsync<T>(
            Expression<Func<T, bool>> condition,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            Expression<Func<T, object>> orderBy,
            bool orderByDescending,
            bool asNoTracking) where T : class => await GetAsync(condition, includes, orderBy, orderByDescending, asNoTracking, CancellationToken.None);


        public async Task<T> GetAsync<T>(
            Expression<Func<T, bool>> condition,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            Expression<Func<T, object>> orderBy = null,
            bool orderByDescending = false,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default)
           where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (orderBy != null)
            {
                query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
            }

            return await query.AsSplitQuery().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjectedType> GetAsync<T, TProjectedType>(
            Expression<Func<T, bool>> condition,
            Expression<Func<T, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default)
            where T : class
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.Select(selectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<bool> ExistsAsync<T>(CancellationToken cancellationToken = default)
           where T : class
        {
            return ExistsAsync<T>(null, cancellationToken);
        }

        public async Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
           where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition == null)
            {
                return await query.AnyAsync(cancellationToken);
            }

            bool isExists = await query.AnyAsync(condition, cancellationToken).ConfigureAwait(false);
            return isExists;
        }
        public async Task<int> GetCountAsync<T>(CancellationToken cancellationToken = default)
            where T : class
        {
            int count = await _dbContext.Set<T>().CountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> GetCountAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.AsNoTracking().CountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync<T>(IEnumerable<Expression<Func<T, bool>>> conditions, CancellationToken cancellationToken = default)
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (conditions != null)
            {
                foreach (Expression<Func<T, bool>> expression in conditions)
                {
                    query = query.Where(expression);
                }
            }

            return await query.CountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> GetLongCountAsync<T>(CancellationToken cancellationToken = default)
            where T : class
        {
            long count = await _dbContext.Set<T>().LongCountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<long> GetLongCountAsync<T>(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> GetLongCountAsync<T>(IEnumerable<Expression<Func<T, bool>>> conditions, CancellationToken cancellationToken = default)
            where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (conditions != null)
            {
                foreach (Expression<Func<T, bool>> expression in conditions)
                {
                    query = query.Where(expression);
                }
            }
            return await query.LongCountAsync(cancellationToken);
        }
        public async Task<List<TR>> GetSelectedPropertiesListAsync<T, TR>(
            Expression<Func<T, bool>> condition,
            Expression<Func<T, TR>> selector,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes,
            bool asNoTracking) where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (condition != null)
                query = query.Where(condition);
            if (includes != null)
                query = includes(query);
            if (asNoTracking)
                query = query.AsNoTracking();

            return await query.AsSplitQuery().Select(selector).ToListAsync();
        }
    }

    public class ProcessMangerQueryRepository : QueryRepository<AppDbContext>, IProcessMangerQueryRepository
    {
        public ProcessMangerQueryRepository(AppDbContext context) : base(context)
        {

        }
    }

}


