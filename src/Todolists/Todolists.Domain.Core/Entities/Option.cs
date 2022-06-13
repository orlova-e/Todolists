using Todolists.Domain.Core.Interfaces;

namespace Todolists.Domain.Core.Entities
{
    public sealed class Option : IDomainEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool Checked { get; set; }
        public string Text { get; set; }
    }
}
