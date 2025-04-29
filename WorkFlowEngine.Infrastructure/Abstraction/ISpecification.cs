using System.Linq.Expressions;

namespace WorkFlowEngine.Infrastructure.Abstraction
{
    public interface ISpecification<T>
    {
        public int? Skip { get; set; }
        public int? Take { get; set; }
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        public (string ColumnName, string SortDirection) OrderByDynamic { get; }
    }
}
