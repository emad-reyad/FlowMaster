using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Application.Features.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.OpenTask
{
    public class OpenTaskHandler : BaseCommandHandler, IRequestHandler<OpenTaskRequest, Result<bool>>
    {
        public OpenTaskHandler(IProcessMangerCommandReposistory commandRepo, IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(commandRepo, mapper, queryRepo, dataCash, instanceAccess)
        {

        }

        public async Task<Result<bool>> Handle(OpenTaskRequest request, CancellationToken cancellationToken)
        {
            var userGroups = _dataCash.GetUserGroups(request.UserId);

            if (userGroups is null || userGroups?.Count == 0)
                return new Result<bool>(false, new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized),Constants.Error.NotAuthorized)
                });

            var instance = await _queryRepo.GetAsync<ProcessInstance>(
                condition: s => s.Id == request.ProcessInstanceId && s.IsActive,
                includes: source => source.
                    Include(s => s.ProcessInstanceUsers),
                false);
            var hasAccess = _instanceAccess.HasInstanceAccess(instance, request.UserId, userGroups);
            if (!hasAccess.IsSuccess)
                return new Result<bool>(false, hasAccess.Errors);
            if (!hasAccess.Data)
                return new Result<bool>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string>(nameof(Constants.Error.NotAuthorized), Constants.Error.NotAuthorized)});

            var result = instance.OpenTask(request.UserId);
            if (result.IsSuccess)
            {
                _commandRepo.Update(instance);
                return new Result<bool>(true, true);
            }
            else
                return new Result<bool>(false, result.Errors);
        }
    }
}
