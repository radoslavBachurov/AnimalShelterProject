namespace AnimalShelter.Web.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class OrderByParameter
    {
        // makes expression for specific prop
        public static Expression<Func<TSource, object>> GetExpression<TSource>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TSource), "x");
            Expression conversion = Expression.Convert(Expression.Property(param, propertyName), typeof(object));   // important to use the Expression.Convert
            return Expression.Lambda<Func<TSource, object>>(conversion, param);
        }

        // makes deleget for specific prop
        public static Func<TSource, object> GetFunc<TSource>(string propertyName)
        {
            return GetExpression<TSource>(propertyName).Compile();  // only need compiled expression
        }

        // OrderBy overload
        public static IOrderedEnumerable<TSource>
        OrderBy<TSource>(this IEnumerable<TSource> source, string propertyName, string orderType)
        {
            if (orderType == "asc")
            {
                return source.OrderBy(GetFunc<TSource>(propertyName));
            }
            else
            {
                return source.OrderByDescending(GetFunc<TSource>(propertyName));
            }
        }

        // OrderBy overload
        public static IOrderedQueryable<TSource>
               OrderBy<TSource>(this IQueryable<TSource> source, string propertyName, string orderType)
        {
            if (orderType == "asc")
            {
                return source.OrderBy(GetExpression<TSource>(propertyName));
            }
            else
            {
                return source.OrderByDescending(GetExpression<TSource>(propertyName));
            }
        }
    }
}
