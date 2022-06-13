using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Web.API.Services.Commands.User;

public class CreateUserAccountCommand : IRequestHandler<CreateUserAccountRequest, HandlerResult<Guid>>
{
    private readonly ILogger<CreateUserAccountCommand> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly IEncryptionService _encryptionService;
    private readonly IRepository _repository;

    public CreateUserAccountCommand(
        ILogger<CreateUserAccountCommand> logger,
        IDateTimeService dateTimeService,
        IEncryptionService encryptionService,
        IRepository repository)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _encryptionService = encryptionService;
        _repository = repository;
    }

    public async Task<HandlerResult<Guid>> Handle(
        CreateUserAccountRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = new Domain.Core.Entities.User
            {
                Name = request.Dto.Name
            };
            
            _dateTimeService.Created(user);

            var account = new Account
            {
                Email = request.Dto.Email,
                Hash = _encryptionService.ComputeHash(request.Dto.Password),
                User = user
            };

            await _repository.CreateAsync<Account, Guid>(account, cancellationToken);
            return HandlerResult<Guid>.Success(user.Id);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't create an account and user:\n{message}\n{stackTrace}",
                exc.Message, exc.StackTrace);
            
            return HandlerResult<Guid>.Exception();
        }
    }
}