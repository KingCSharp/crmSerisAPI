using crmSeries.Core.Logic.Queries;
using System.Linq;

namespace crmSeries.Core.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> GetPagedData<T>(
            this IQueryable<T> queryable,
            PagedQueryRequest request)
        {
            return queryable.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);
        }
    }
}