using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Infrastructure.DataAccess;
using Todolists.Services.Messaging.Interfaces;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.Dtos.Checklist;

namespace Todolists.Web.API.Services.Commands.Notes;

public class CreateChecklistCommand : IRequestHandler<CreateChecklistRequest, HandlerResult<Guid>>
{
    private readonly ILogger<CreateChecklistCommand> _logger;
    private readonly IDateTimeService _dateTimeService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMessageService _messageService;
    private readonly ITranslator _translator;
    private readonly IRepository _repository;

    public CreateChecklistCommand(
        ILogger<CreateChecklistCommand> logger,
        IDateTimeService dateTimeService,
        ICurrentUserService currentUserService,
        IMessageService messageService,
        ITranslator translator,
        IRepository repository)
    {
        _logger = logger;
        _dateTimeService = dateTimeService;
        _currentUserService = currentUserService;
        _messageService = messageService;
        _translator = translator;
        _repository = repository;
    }

    public async Task<HandlerResult<Guid>> Handle(
        CreateChecklistRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var entity = _translator.Translate<ChecklistCreateDto, Checklist>(request.Dto);
            var user = await _currentUserService.GetAsync(cancellationToken);
            entity.User = user;
            
            _dateTimeService.Created(entity);
            await _repository.CreateAsync<Checklist, Guid>(entity, cancellationToken);

            var checklistCreatedDto = _translator.Translate<Checklist, ChecklistCreateDto>(entity);
            _messageService.Send("create", checklistCreatedDto);
            
            return HandlerResult<Guid>.Success(entity.Id);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't create a checklist:\n{message}\n{stackTrace}",
                 exc.Message, exc.StackTrace);
            
            return HandlerResult<Guid>.Exception();
        }
    }
}