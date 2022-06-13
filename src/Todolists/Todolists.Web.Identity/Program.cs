using Todolists.Infrastructure.Base.Configuration;
using Todolists.Infrastructure.DataAccess.Configuration;
using Todolists.Services.Shared.Configuration;
using Todolists.Web.Identity.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services
    .AddBaseInfrastructure(builder.Configuration)
    .AddCommonInfrastructure()
    .AddSharedServices()
    .AddIdentityServices(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app
    .UseRouting()
    .UseAuthentication()
    .UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("");
    });
});

app.Run();