using FluentValidation;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Todolists.Web.Identity.Application;
using Todolists.Web.Identity.Dto;
using Todolists.Web.Identity.Models;
using Todolists.Web.Identity.Validation;

namespace Todolists.Web.Identity.Configuration
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));
            var identityOptions = configuration.GetSection(nameof(IdentityOptions)).Get<IdentityOptions>();

            var keysDirectory = Path.Combine(webHostEnvironment.ContentRootPath, "Keys");

            services
                .AddScoped<IValidator<LoginDto>, LoginDtoValidator>()
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddResourceOwnerValidator<OwnerValidator>()
                .AddInMemoryApiScopes(IdentityConfiguration.GetApiScopes(identityOptions))
                .AddInMemoryClients(IdentityConfiguration.GetClients(identityOptions))
                .AddInMemoryApiResources(IdentityConfiguration.GetApiResources()).Services
                .AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysDirectory))
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                {
                    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                });

            return services;
        }
    }
}
