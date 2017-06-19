using System;
using System.Linq;
using System.Linq.Expressions;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MVCGrid.Web.Models
{
    public static class Extensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> source,
            Expression<Func<TSource, TKey>> keySelector,
            SortDirection order)
        {
            switch (order)
            {
                case SortDirection.Unspecified:
                case SortDirection.Asc: return source.OrderBy(keySelector);
                case SortDirection.Dsc:
                    return source.OrderByDescending(keySelector);
            }

            throw new ArgumentOutOfRangeException("order");
        }
    }
}