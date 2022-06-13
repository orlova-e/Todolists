namespace Todolists.Web.Identity.Models;

public class IdentityClient
{
    public string ClientId { get; set; }
    public string AllowedScope { get; set; }
    public bool RequireClientSecret { get; set; }
    public bool AllowOfflineAccess { get; set; }
    public int AccessTokenLifetimeHours { get; set; }
}