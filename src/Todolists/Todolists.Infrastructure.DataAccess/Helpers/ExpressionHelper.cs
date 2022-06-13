using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Todolists.Domain.Services.Helpers;

namespace Todolists.Infrastructure.DataAccess.Helpers;

public static class ExpressionHelper
{
    public static Expression<Func<T, bool>> Build<T>(string filter)
    {
        Expression<Func<T, bool>> expression = null;
        if (FilterParser.TryParseFilter(filter, out Dictionary<string, string> filters))
        {
            for (int i = 0; i < filters.Count; i++)
            {
                var right = ExpressionHelper.BuildExpression<T>(
                    filters.ElementAt(i).Key,
                    filters.ElementAt(i).Value);
                    
                if (i == 0)
                {
                    expression = right;
                    continue;
                }

                expression = PredicateBuilder.And(expression, right);
            }
        }

        return expression;
    }

    static Expression<Func<T, bool>> BuildExpression<T>(string propertyName, string value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        
        var property = Expression.Property(parameter, propertyName);
        var constant = GetValueExpression(propertyName, value, parameter);
        
        var body = Expression.Equal(property, constant);
        var final = Expression.Lambda<Func<T, bool>>(body, parameter);
        
        return final;
    }

    private static UnaryExpression GetValueExpression(string propertyName, string value, ParameterExpression parameter)
    {
        var member = Expression.Property(parameter, propertyName);
        var propertyType = ((PropertyInfo) member.Member).PropertyType;
        
        var converter = TypeDescriptor.GetConverter(propertyType);
        if (!converter.CanConvertFrom(typeof(string)))
            throw new NotSupportedException();
        var propertyValue = converter.ConvertFromInvariantString(value);
        
        var constant = Expression.Constant(propertyValue);
        return Expression.Convert(constant, propertyType);
    }
}