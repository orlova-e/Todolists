namespace Todolists.Web.API.Models;

public class IdentityOptions
{
    public string Address { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
}