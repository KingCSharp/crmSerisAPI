using System;
using System.Linq.Expressions;
using System.Reflection;

namespace crmSeries.Core.Extensions
{
    public static class ExpressionExtensions
    {
        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(this Expression<Func<TSource, TProperty>> propertyLambda)
        {
            MemberExpression memberExpr = (propertyLambda.Body.NodeType == ExpressionType.Convert)
                ? ((UnaryExpression)propertyLambda.Body).Operand as MemberExpression
                : propertyLambda.Body as MemberExpression;

            if (memberExpr == null)
                throw new ArgumentException($"Expression '{propertyLambda}' is not a member expression.");

            var propInfo = memberExpr.Member as PropertyInfo
                ?? throw new ArgumentException($"Expression '{propertyLambda}' refers to a field, not a property.");
            
            return propInfo;
        }
    }
}
