using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.IsWorkItemAvailable
{
    internal class IsWorkItemAvailableHandler : BaseQueryHandler, IRequestHandler<IsWorkItemAvailableRequest, Result<bool>>
    {
        public IsWorkItemAvailableHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        public async Task<Result<bool>> Handle(IsWorkItemAvailableRequest request, CancellationToken cancellationToken)
        {
            List<string> userGroups = _dataCash.GetUserGroups(request.UserId);

            var isAvailable = false;
            if (userGroups is null || userGroups.Count == 0)
                return new Result<bool>(isAvailable, false);
            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition:
                    s => s.Id == request.ProcessInstanceId && s.IsActive,
                includes:
                    s => s.Include(h => h.ProcessInstanceUsers), true);

            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroups);
            if (!hasAccess.IsSuccess)
                return new Result<bool>(false, hasAccess.Errors);
            if (!hasAccess.Data)
                return new Result<bool>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized), Constants.Error.NotAuthorized)});
            isAvailable = instance.Opened && instance.LastModifiedBy == request.UserId;
            if (!isAvailable)
                isAvailable = instance.ProcessInstanceUsers.Any(s => s.IsActive && userGroups.Contains(s.DestinationName));
            return new Result<bool>(isAvailable, true);
        }
    }
}
