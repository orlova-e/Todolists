using Todolists.Domain.Core.Interfaces;

namespace Todolists.Domain.Core.Entities
{
    public abstract class NoteBase : IDomainEntity, IHistorical
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
