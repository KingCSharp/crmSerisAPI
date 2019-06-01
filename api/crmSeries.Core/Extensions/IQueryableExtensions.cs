using crmSeries.Core.Logic.Queries;
using System;
using System.Collections;
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

        public static bool RelatedEntityExists<T>(this IQueryable<T> set, Func<T, bool> expression)
            where T: class 
        {
            return set.Any(expression);
        }
    }
}