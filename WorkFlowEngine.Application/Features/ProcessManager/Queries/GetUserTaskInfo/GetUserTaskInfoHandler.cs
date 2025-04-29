using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserTaskInfo
{
    public class GetUserTaskInfoHandler : BaseQueryHandler, IRequestHandler<GetUserTaskInfoRequest, Result<GetUserTaskInfoResponse>>
    {
        public GetUserTaskInfoHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {

        }

        public async Task<Result<GetUserTaskInfoResponse>> Handle(GetUserTaskInfoRequest request, CancellationToken cancellationToken)
        {
            List<string> userGroups = _dataCash.GetUserGroups(request.UserId);


            if (userGroups is null || userGroups.Count == 0)
                return new Result<GetUserTaskInfoResponse>(new GetUserTaskInfoResponse(default!, default!, default!), true);
            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition: s => s.ApplicationNumber == request.ApplicationNumber
                    && s.IsActive, //&& 
                                   //s.ProcessInstanceUsers.Any(s => userGroups.Contains(s.DestinationName)),
                includes: source => source.Include(s => s.CurrentActivity).
                Include(s => s.ProcessInstanceUsers),
                asNoTracking: true);
            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroups);
            if (!hasAccess.IsSuccess)
                return new Result<GetUserTaskInfoResponse>(false, hasAccess.Errors);
            if (!hasAccess.Data)
                return new Result<GetUserTaskInfoResponse>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized), Constants.Error.NotAuthorized)});


            //if (instance is null || instance.CurrentActivity is null)
            //    return new Result<GetTaskInfoResponse>(false, new List<KeyValuePair<string, string>>
            //    {
            //        new KeyValuePair<string, string>(nameof(Constants.Error.NotFound),Constants.Error.NotFound)
            //    });
            var result = _mapper.Map<GetUserTaskInfoResponse>(instance);
            return new Result<GetUserTaskInfoResponse>(result, true);
        }
    }
}
