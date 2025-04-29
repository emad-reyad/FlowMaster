using AutoMapper;
using MediatR;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserWorklistStatistics
{
    public class GetUserWorklistHandler : BaseQueryHandler, IRequestHandler<GetUserWorklistRequest, Result<List<GetUserWorklistResponse>>>
    {
        public GetUserWorklistHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        async Task<Result<List<GetUserWorklistResponse>>> IRequestHandler<GetUserWorklistRequest, Result<List<GetUserWorklistResponse>>>.Handle(GetUserWorklistRequest request, CancellationToken cancellationToken)
        {
            List<string> userGroups = _dataCash.GetUserGroups(request.UserId);

            if (userGroups is null || userGroups.Count == 0)
                return new Result<List<GetUserWorklistResponse>>(null, true);

            var dateFrom = request.DateFrom == null ? DateTime.MinValue : request.DateFrom;
            var dateTo = request.DateTo == null ? DateTime.MaxValue : request.DateTo;
            var result = await _queryRepo.GetSelectedPropertiesListAsync<ProcessInstance, GetUserWorklistResponse>(
                condition:
                    s => s.IsActive && (s.CreationDate >= dateFrom && s.CreationDate <= dateTo)
                    &&
                    (
                        (s.Opened && s.LastModifiedBy == request.UserId)
                    ),
                asNoTracking: true,
                includes: null,
                selector: s => new GetUserWorklistResponse(s.Id, s.Opened));
            result.AddRange(await _queryRepo.GetSelectedPropertiesListAsync<ProcessInstance, GetUserWorklistResponse>(
                condition:
                     s => s.IsActive && (s.CreationDate >= dateFrom && s.CreationDate <= dateTo)
                     &&
                    (
                        (!s.Opened && s.ProcessInstanceUsers.Any(u => u.IsActive && userGroups.Contains(u.DestinationName)))
                    ),
                asNoTracking: true,
                includes: null,
                selector: s => new GetUserWorklistResponse(s.Id, s.Opened)));
            //var worklist = _mapper.Map<List<GetUserWorklistResponse>>(dbResult);
            return new Result<List<GetUserWorklistResponse>>(result, true);
        }
    }
}
