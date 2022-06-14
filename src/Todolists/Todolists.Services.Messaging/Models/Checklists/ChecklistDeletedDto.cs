namespace Todolists.Services.Messaging.Models.Checklists;

public class ChecklistDeletedDto
{
    public Guid Id { get; init; }
    public DateTime Deleted { get; init; }
}