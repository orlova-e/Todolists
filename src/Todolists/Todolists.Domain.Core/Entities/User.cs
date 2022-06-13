using Todolists.Domain.Core.Interfaces;

namespace Todolists.Domain.Core.Entities
{
    public sealed class User : IDomainEntity, IHistorical
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? Deleted { get; set; }
        public Account Account { get; set; }
    }
}
