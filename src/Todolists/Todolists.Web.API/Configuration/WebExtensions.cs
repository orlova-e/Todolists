using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Todolists.Web.API.Services.PipelineBehaviours;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.API.Models;
using Todolists.Web.API.Services.Commands.User;
using Todolists.Web.API.Services.Implementation;
using Todolists.Web.API.Services.Validation.User;

namespace Todolists.Web.API.Configuration;

public static class WebExtensions
{
    public static IServiceCollection AddWebServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = configuration
            .GetSection(nameof(IdentityOptions))
            .Get<IdentityOptions>();
        
        services
            .AddAutoMapper(typeof(Program).Assembly)
            .AddValidatorsFromAssemblyContaining<CreateUserAccountDtoValidator>()
            .AddMediatR(typeof(CreateUserAccountRequest).Assembly)
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(AccessorBehavior<,>))
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = identityOptions.Address;
                options.RequireHttpsMetadata = identityOptions.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = identityOptions.ValidateAudience,
                    ValidateIssuer = identityOptions.ValidateIssuer
                };
            })
            .Services
            .AddAuthorization()
            .AddTransient<ICurrentUserService, CurrentUserService>()
            .AddHttpContextAccessor()
            .TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        
        return services;
    }
}