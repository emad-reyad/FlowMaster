using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.IsTaskAvailable
{
    internal class IsTaskAvailableHandler : BaseQueryHandler, IRequestHandler<IsTaskAvailableRequest, Result<int>>
    {
        public IsTaskAvailableHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        public async Task<Result<int>> Handle(IsTaskAvailableRequest request, CancellationToken cancellationToken)
        {
            List<string> userGroups = _dataCash.GetUserGroups(request.UserId);

            var isAvailable = false;
            if (userGroups is null || userGroups.Count == 0)
                return new Result<int>(0, isAvailable);
            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition:
                    s => s.ApplicationNumber == request.ApplicationNumber && s.IsActive,
                includes:
                    s => s.Include(h => h.ProcessInstanceUsers),
               orderBy: q => q.Id,
               true,
               true);

            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroups);
            if (!hasAccess.IsSuccess)
                return new Result<int>(false, hasAccess.Errors);
            if (!hasAccess.Data)
                return new Result<int>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized), Constants.Error.NotAuthorized)});
            isAvailable = instance.Opened && instance.LastModifiedBy == request.UserId;
            if (!isAvailable)
                isAvailable = instance.ProcessInstanceUsers.Any(s => s.IsActive && userGroups.Contains(s.DestinationName));
            return new Result<int>(instance.Id, true);
        }
    }
}
