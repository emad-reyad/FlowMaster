using System.Linq.Expressions;
using WorkFlowEngine.Infrastructure.Abstraction;

namespace WorkFlowEngine.Infrastructure.Repositories
{
    public class Specification<T> : ISpecification<T>
        where T : class
    {
        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } =
                                               new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        public (string ColumnName, string SortDirection) OrderByDynamic { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        // string-based includes allow for including children of children
        // e.g. Basket.Items.Product
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }

}
