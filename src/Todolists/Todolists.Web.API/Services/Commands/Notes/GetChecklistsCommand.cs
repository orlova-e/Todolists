using MediatR;
using Todolists.Domain.Core.Entities;
using Todolists.Domain.Services.Helpers;
using Todolists.Infrastructure.DataAccess;
using Todolists.Infrastructure.DataAccess.Helpers;
using Todolists.Services.Shared.Interfaces;
using Todolists.Web.Dtos.Checklist;
using Todolists.Web.Dtos.Shared;

namespace Todolists.Web.API.Services.Commands.Notes;

public class GetChecklistsCommand : IRequestHandler<GetChecklistsRequest, HandlerResult<ListDto<ChecklistGetDto>>>
{
    private readonly ILogger<GetChecklistsCommand> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly ITranslator _translator;
    private readonly IRepository _repository;

    public GetChecklistsCommand(
        ILogger<GetChecklistsCommand> logger,
        ICurrentUserService currentUserService,
        ITranslator translator,
        IRepository repository)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _translator = translator;
        _repository = repository;
    }

    public async Task<HandlerResult<ListDto<ChecklistGetDto>>> Handle(
        GetChecklistsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _currentUserService.GetAsync(cancellationToken);
            
            var filterExpression = ExpressionHelper.Build<Checklist>(request.Dto.Filters);
            var userExpression = Domain.Services.Specs.Domain.Notes<Checklist>.ByUser(user.Id).Expression;
            var expression = filterExpression != null ? userExpression.And(filterExpression) : userExpression;

            var entities = await _repository.ListAsync<Checklist, Guid>(
                wherePredicate: expression,
                orderBy: request.Dto.OrderBy ?? nameof(NoteBase.Created),
                sortDir: request.Dto.SortDir,
                skip: request.Dto.ItemsNumber * (request.Dto.Page - 1),
                take: request.Dto.ItemsNumber,
                cancellationToken);
            
            var totalEntitiesCount = await _repository.CountAsync<Checklist, Guid>(expression, cancellationToken);
            
            var dtos = _translator.Translate<IEnumerable<Checklist>, IEnumerable<ChecklistGetDto>>(entities);

            var listDto = new ListDto<ChecklistGetDto>
            {
                CurrentPage = request.Dto.Page,
                ItemsPerPage = request.Dto.ItemsNumber,
                ListSorting = request.Dto.SortDir,
                TotalPages = (int) Math.Ceiling((double) totalEntitiesCount / request.Dto.ItemsNumber),
                TotalItems = totalEntitiesCount,
                Entities = dtos,
                Filters = request.Dto.Filters,
            };
            
            return HandlerResult<ListDto<ChecklistGetDto>>.Success(listDto);
        }
        catch (Exception exc)
        {
            _logger.LogError("Couldn't get checklists:\n{message}\n{stackTrace}",
                 exc.Message, exc.StackTrace);
            
            return HandlerResult<ListDto<ChecklistGetDto>>.Exception();
        }
    }
}