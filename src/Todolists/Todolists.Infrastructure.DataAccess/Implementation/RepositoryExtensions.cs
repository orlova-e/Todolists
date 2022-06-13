using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Todolists.Domain.Core.Conditions;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Core.Interfaces;

namespace Todolists.Infrastructure.DataAccess.Implementation;

internal static class RepositoryExtensions
{
    public static IQueryable<TEntity> MatchInclude<TEntity, TKey>(this IQueryable<TEntity> source)
        where TEntity : class, IEntity<TKey>
    {
        return source switch
        {
            IQueryable<Account> accounts => (IQueryable<TEntity>) accounts.Include(x => x.User),
            IQueryable<User> users => (IQueryable<TEntity>) users.Include(x => x.Account),
            IQueryable<Checklist> checklists => (IQueryable<TEntity>) checklists.Include(x => x.Options)
                .Include(x => x.User),
            IQueryable<Option> options => (IQueryable<TEntity>) options.IgnoreQueryFilters(),
            _ => source
        };
    }

    public static IQueryable<T> OrderBy<T>(
        this IQueryable<T> source,
        string orderByProperty,
        SortDir sortDir)
    {
        var type = typeof(T);
        var property = type.GetProperty(orderByProperty);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        var orderMethodName = sortDir == SortDir.Asc ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending);
        
        MethodCallExpression resultExpression = Expression.Call(
            typeof(Queryable),
            orderMethodName,
            new Type[] { type, property.PropertyType },
            source.Expression,
            Expression.Quote(orderByExpression));
        
        return source.Provider.CreateQuery<T>(resultExpression);
    }
}