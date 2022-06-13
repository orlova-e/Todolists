using Todolists.Domain.Core.Interfaces;

namespace Todolists.Domain.Core.Entities
{
    public sealed class Account : IDomainEntity, IHistorical
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}