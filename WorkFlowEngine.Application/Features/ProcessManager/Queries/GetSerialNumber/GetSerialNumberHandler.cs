using AutoMapper;
using MediatR;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetSerialNumber
{
    internal class GetSerialNumberHandler : BaseQueryHandler, IRequestHandler<GetSerialNumberRequest, Result<int>>
    {
        public GetSerialNumberHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        public async Task<Result<int>> Handle(GetSerialNumberRequest request, CancellationToken cancellationToken)
        {
            var instance = await _queryRepo.GetAsync<ProcessInstance>(
              condition: s => s.ApplicationNumber == request.ApplicationNumber && s.IsActive,
              asNoTracking: true);
            if (instance is null)
                return new Result<int>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotFound), Constants.Error.NotFound)});

            return new Result<int>(instance.Id, true);
        }
    }
}
