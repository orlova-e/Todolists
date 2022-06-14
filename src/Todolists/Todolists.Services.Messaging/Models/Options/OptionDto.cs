namespace Todolists.Services.Messaging.Models.Options;

public class OptionDto
{
    public Guid Id { get; init; }
    public bool Checked { get; init; }
    public string Text { get; init; }
}