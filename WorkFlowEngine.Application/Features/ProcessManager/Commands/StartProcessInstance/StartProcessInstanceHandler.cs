using AutoMapper;
using MediatR;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Application.Features.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance
{

    public sealed class StartProcessInstanceHandler : BaseCommandHandler, IRequestHandler<StartProcessInstanceRequest,
        Result<StartProcessInstanceResponse>>
    {
        public StartProcessInstanceHandler(IProcessMangerCommandReposistory repository, IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(repository, mapper, queryRepo, dataCash, instanceAccess)
        {
        }


        public async Task<Result<StartProcessInstanceResponse>> Handle(StartProcessInstanceRequest request, CancellationToken cancellationToken)
        {
            var process = await _dataCash.GetProcessByName(request.ProcessName);
            if (process is null)
                return new Result<StartProcessInstanceResponse>(false, new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(nameof(Constants.Error.NoProcess), Constants.Error.NoProcess) });
            _commandRepo.Attch(process, true);
            var result = new ProcessInstance(process).Start(request.DataFields, request.UserId, request.ApplicationNumber, request.ApplicantNumber);

            if (result.IsSuccess)
            {
                var instance = _commandRepo.Add(result.Data);
                var response = _mapper.Map<StartProcessInstanceResponse>(instance);
                return new Result<StartProcessInstanceResponse>(response, true);
            }
            return new Result<StartProcessInstanceResponse>(false, result.Errors);
        }
    }
}
