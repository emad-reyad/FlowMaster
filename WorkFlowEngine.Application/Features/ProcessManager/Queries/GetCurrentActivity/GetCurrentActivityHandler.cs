using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetCurrentActivity
{
    public class GetCurrentActivityHandler : BaseQueryHandler, IRequestHandler<GetCurrentActivityRequest, Result<GetCurrentActivityResponse>>
    {
        public GetCurrentActivityHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        public async Task<Result<GetCurrentActivityResponse>> Handle(GetCurrentActivityRequest request, CancellationToken cancellationToken)
        {
            var instance = await _queryRepo.GetAsync<ProcessInstance>(condition: s => s.Id == request.ProcessInstanceId,
                includes: source => source.Include(s => s.CurrentActivity),
                asNoTracking: true);
            if (instance is null || instance.CurrentActivity is null)
                return new Result<GetCurrentActivityResponse>(false, new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotFound),Constants.Error.NotFound)
                });
            var result = _mapper.Map<GetCurrentActivityResponse>(instance.CurrentActivity);
            return new Result<GetCurrentActivityResponse>(result, true);
        }
    }
}
