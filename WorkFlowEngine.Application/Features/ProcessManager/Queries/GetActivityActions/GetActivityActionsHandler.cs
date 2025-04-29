using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetActivityActions
{
    public class GetActivityActionsHandler : BaseQueryHandler, IRequestHandler<GetActivityActionsRequest, Result<List<string>>>
    {
        public GetActivityActionsHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        async Task<Result<List<string>>> IRequestHandler<GetActivityActionsRequest, Result<List<string>>>.Handle(GetActivityActionsRequest request, CancellationToken cancellationToken)
        {
            var instance = await _queryRepo.GetAsync<ProcessInstance>(
               condition: s => s.Id == request.ProcessInstanceId && s.IsActive,
               includes: source =>
               source.Include(s => s.CurrentActivity).
                    ThenInclude(s => s.ActivityActions).
                        ThenInclude(s => s.Action).
                    Include(s => s.ProcessInstanceUsers),
               asNoTracking: true);
            var userGroups = _dataCash.GetUserGroups(request.UserId);
            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroups);
            if (!hasAccess.IsSuccess)
                return new Result<List<string>>(false, hasAccess.Errors);
            if (!hasAccess.Data)
                return new Result<List<string>>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized), Constants.Error.NotAuthorized)});

            if (!instance.CurrentActivity.ActivityActions.Any())
                return new Result<List<string>>(false,
                    new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(nameof(Constants.Error.NotFound), Constants.Error.NotFound) });
            var result = instance.CurrentActivity.ActivityActions.Select(s => s.Action.Name).ToList();
            return new Result<List<string>>(result, true);
        }
    }
}
