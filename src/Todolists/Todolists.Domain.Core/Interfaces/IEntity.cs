namespace Todolists.Domain.Core.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
        bool IsDeleted { get; set; }
    }
}
