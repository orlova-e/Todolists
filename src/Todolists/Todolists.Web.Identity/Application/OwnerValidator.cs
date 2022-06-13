using System.Security.Claims;
using FluentValidation;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.Identity.Dto;

namespace Todolists.Web.Identity.Application;

public class OwnerValidator : IResourceOwnerPasswordValidator
{
    private readonly ILogger<OwnerValidator> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly IValidator<LoginDto> _validator;
    private readonly IRepository _repository;

    public OwnerValidator(
        ILogger<OwnerValidator> logger,
        IDateTimeService dateTimeService,
        IValidator<LoginDto> validator,
        IRepository repository)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _validator = validator;
        _repository = repository;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var dto = new LoginDto
        {
            Name = context.UserName,
            Password = context.Password
        };

        var results = await _validator.ValidateAsync(dto);
        if (!results.IsValid)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidClient);
            _logger.LogInformation("Failed validation: {name}", dto.Name);
            
            return;
        }

        var account = await _repository.GetAsync<Account, Guid>(
            Accounts.ByEmailOrUsername(dto.Name) &
            Common.NotDeleted<Account>(), 
            CancellationToken.None);
        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.User.Id.ToString()),
            new("client_id", "client")
        };

        context.Result = new GrantValidationResult(
            subject: account.User.Id.ToString(),
            authTime: _dateTimeService.UtcNow,
            authenticationMethod: "password",
            claims: claims
        );
    }
}