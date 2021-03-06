using Microsoft.AspNetCore.HttpOverrides;
using Todolists.Infrastructure.Base.Configuration;
using Todolists.Infrastructure.DataAccess.Configuration;
using Todolists.Services.Messaging.Configuration;
using Todolists.Services.Shared.Configuration;
using Todolists.Web.API.Configuration;
using Todolists.Web.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddBaseInfrastructure(builder.Configuration)
    .AddCommonInfrastructure()
    .AddSharedServices(typeof(Program).Assembly)
    .AddMessagingServices(builder.Configuration)
    .AddWebServices(builder.Configuration);

var app = builder.Build();

app
    .UseForwardedHeaders(new ForwardedHeadersOptions 
        {ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto})
    .UseRouting()
    .UseHttpsRedirection()
    .UseSession()
    .UseLogging()
    .UseCorrelationIds()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

app.Run();