using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.ExecuteActivityAction
{
    public sealed class ExecuteActivityActionHandler : BaseCommandHandler, IRequestHandler<ExecuteActivityActionRequest, Result<ExecuteActivityActionResponse>>
    {

        public ExecuteActivityActionHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IProcessMangerCommandReposistory commandRepo, IDataCash dataCash, IAdHelpper adHelper, IInstanceAccess instanceAccess) : base(commandRepo, mapper, queryRepo, dataCash, instanceAccess)
        {
        }


        public async Task<Result<ExecuteActivityActionResponse>> Handle(ExecuteActivityActionRequest request, CancellationToken cancellationToken)
        {

            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition: s => s.Id == request.ProcessInstanceId && s.IsActive,
                includes: source => source.
                    Include(s => s.ProcessInstanceUsers.Where(h => h.IsActive)).
                    Include(S => S.ProcessInstanceDataFields),
                    //.Include(s => s.ProcessInstanceHistories)
                    false);

            var userGroups = _dataCash.GetUserGroups(request.UserId);
            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroups);
            if (!hasAccess.IsSuccess)
                return new Result<ExecuteActivityActionResponse>(false, hasAccess.Errors);

            if (!hasAccess.Data)
                return new Result<ExecuteActivityActionResponse>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized), Constants.Error.NotAuthorized)});

            var process = await _dataCash.GetProcessById(instance.ProcessId);

            _commandRepo.Attch(process, true);
            instance.SetProcess(process);

            var result = instance.ExecuteActivityAction(request.UserId, request.Action, request.DataFields);

            if (result.IsSuccess)
            {
                _commandRepo.Update<ProcessInstance>(instance);
                var response = _mapper.Map<ExecuteActivityActionResponse>(instance);
                return new Result<ExecuteActivityActionResponse>(response, true);
            }
            return new Result<ExecuteActivityActionResponse>(false, result.Errors);
        }
    }
}
