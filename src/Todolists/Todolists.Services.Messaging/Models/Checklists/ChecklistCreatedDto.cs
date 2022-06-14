using Todolists.Services.Messaging.Models.Options;

namespace Todolists.Services.Messaging.Models.Checklists;

public class ChecklistCreatedDto
{
    public Guid Id { get; init; }
    public DateTime Created { get; init; }
    public string Name { get; init; }
    public Guid UserId { get; init; }
    public IEnumerable<OptionDto> Options { get; init; }
}