using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Application.Features.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.ReleaseTask
{
    public class ReleaseTaskHandler : BaseCommandHandler, IRequestHandler<ReleaseTaskRequest, Result<bool>>
    {
        public ReleaseTaskHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IProcessMangerCommandReposistory commandQuery, IDataCash dataCash, IInstanceAccess instanceAccess) : base(commandQuery, mapper, queryRepo, dataCash, instanceAccess)
        {
        }
        public async Task<Result<bool>> Handle(ReleaseTaskRequest request, CancellationToken cancellationToken)
        {
            var releaseAction = await _queryRepo.GetAsync<Domain.Entities.Action>(
                condition: s => s.Name == Constants.Actions.Release,
                asNoTracking: true);
            if (releaseAction is null)
                return new Result<bool>(false, new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(nameof(Constants.Error.NoReleaseAction), Constants.Error.NoReleaseAction) });

            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition: s => s.Id == request.ProcessInstanceId,
                includes: source => source.Include(s => s.ProcessInstanceHistories),
                asNoTracking: false);

            if (instance is null)
                return new Result<bool>(false, new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(nameof(Constants.Error.NotFound), Constants.Error.NotFound) });

            //ToDo:Assure that the release by has the authority to cancel Instance.
            var process = await _dataCash.GetProcessById(instance.ProcessId);
            _commandRepo.Attch(process, true);
            instance.SetProcess(process);

            var result = instance.Release(releaseAction, request.UserId);
            if (!result.IsSuccess)
                return new Result<bool>(false, result.Errors);

            _commandRepo.Update<ProcessInstance>(instance);
            return new Result<bool>(true, true);
        }
    }
}
