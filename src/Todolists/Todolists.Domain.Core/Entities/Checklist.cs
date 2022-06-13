namespace Todolists.Domain.Core.Entities
{
    public sealed class Checklist : NoteBase
    {
        public ICollection<Option> Options { get; set; }
    }
}
