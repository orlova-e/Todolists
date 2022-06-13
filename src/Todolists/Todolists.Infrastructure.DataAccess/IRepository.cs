using System.Linq.Expressions;
using Todolists.Domain.Core.Interfaces;
using Todolists.Domain.Core.Conditions;

namespace Todolists.Infrastructure.DataAccess;

public interface IRepository
{
    T Get<T, TKey>(TKey id) where T : class, IEntity<TKey>;

    T Get<T, TKey>(Expression<Func<T, bool>> wherePredicate)
        where T : class, IEntity<TKey>;

    Task<T> GetAsync<T, TId>(TId id, CancellationToken cancellationToken)
        where T : class, IEntity<TId>;

    Task<T> GetAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task<List<T>> ListAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task<List<T>> ListAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, string orderBy,
        SortDir sortDir, int? skip, int? take, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task<int> CountAsync<T, TKey>(Expression<Func<T, bool>> wherePredicate, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task<T> CreateAsync<T, TKey>(T entity, CancellationToken cancellationToken) 
        where T : class, IEntity<TKey>;

    Task CreateAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task UpdateAsync<T, TKey>(T entity, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task UpdateAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task DeleteAsync<T, TKey>(TKey id, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;

    Task DeleteAsync<T, TKey>(IEnumerable<T> entities, CancellationToken cancellationToken)
        where T : class, IEntity<TKey>;
}