namespace Todolists.Web.API.Configuration;

public class SwaggerOptions
{
    public string EndpointPath { get; set; }
    public string EndpointName { get; set; }
    public string OAuthClientId { get; set; }
    public string OAuthAppName { get; set; }

    public string DocName { get; set; }
    public string DocTitle { get; set; }
    public string DocVersion { get; set; }
}