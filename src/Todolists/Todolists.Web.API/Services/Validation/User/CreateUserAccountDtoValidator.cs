using System.Net.Mail;
using System.Text.RegularExpressions;
using FluentValidation;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Specs.Domain;
using Todolists.Infrastructure.DataAccess;
using Todolists.Web.Dtos.Account;

namespace Todolists.Web.API.Services.Validation.User;

internal class CreateUserAccountDtoValidator : AbstractValidator<CreateUserAccountDto>
{
    private readonly IRepository _repository;
    
    public CreateUserAccountDtoValidator(IRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.NameRequired)
            .Must(IsNameValid)
            .Length(4, 20)
            .WithMessage(ValidationMessages.NameLength)
            .WithMessage(ValidationMessages.NameInvalid)
            .MustAsync(NameShouldBeUniqueAsync)
            .WithMessage(ValidationMessages.NameShouldBeUnique);
        
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmailRequired)
            .Must(IsEmailValid)
            .WithMessage(ValidationMessages.EmailInvalid)
            .MustAsync(EmailShouldBeUniqueAsync)
            .WithMessage(ValidationMessages.EmailShouldBeUnique);

        RuleFor(x => x.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(ValidationMessages.PasswordRequired)
            .MinimumLength(8)
            .WithMessage(ValidationMessages.PasswordMinLength)
            .Must(IsPasswordValid)
            .WithMessage(ValidationMessages.PasswordInvalid);
    }

    private bool IsNameValid(string name)
        => Regex.IsMatch(name, @"^[a-zA-Z0-9]+$", RegexOptions.Singleline);
    
    private async Task<bool> NameShouldBeUniqueAsync(
        CreateUserAccountDto dto,
        string name,
        CancellationToken cancellationToken)
    {
        var count = await _repository.CountAsync<Domain.Core.Entities.User, Guid>(
            Users.ByName(name) &
            Common.NotDeleted<Domain.Core.Entities.User>(), cancellationToken);
        
        return count is 0;
    }

    private bool IsEmailValid(string email)
        => MailAddress.TryCreate(email, out _);

    private async Task<bool> EmailShouldBeUniqueAsync(
        CreateUserAccountDto dto,
        string email,
        CancellationToken cancellationToken)
    {
        var count = await _repository.CountAsync<Domain.Core.Entities.Account, Guid>(
            Accounts.ByEmail(dto.Email) &
            Common.NotDeleted<Account>(), cancellationToken);
        
        return count is 0;
    }

    private bool IsPasswordValid(string password)
        => Regex.IsMatch(password, @"[\u0021-\u007E]+", RegexOptions.Singleline);
}