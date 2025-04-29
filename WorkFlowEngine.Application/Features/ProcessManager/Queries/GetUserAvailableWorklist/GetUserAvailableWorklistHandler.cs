using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserAvailableWorklist
{
    internal class GetUserAvailableWorklistHandler : BaseQueryHandler, IRequestHandler<GetUserAvailableWorklistRequest, Result<List<Worklist>>>
    {
        public GetUserAvailableWorklistHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }


        public async Task<Result<List<Worklist>>> Handle(GetUserAvailableWorklistRequest request, CancellationToken cancellationToken)
        {
            List<string> userGroups = _dataCash.GetUserGroups(request.UserId);

            if (userGroups is null || userGroups.Count == 0)
                return new Result<List<Worklist>>(new List<Worklist>(), true);

            Expression<Func<ProcessInstance, bool>> condition = instance => instance.IsActive && !instance.Opened
            && instance.ProcessInstanceUsers.Any(s => s.IsActive && userGroups.Contains(s.DestinationName))
            && (string.IsNullOrEmpty(request.ApplicationNumber) || instance.ApplicationNumber.Contains(request.ApplicationNumber))
            && (string.IsNullOrEmpty(request.ApplicantNumber) || (instance.ApplicantNumber != null && instance.ApplicantNumber.Contains(request.ApplicantNumber)))
            && (!request.DateFrom.HasValue || instance.CreationDate >= request.DateFrom.Value)
            && (!request.DateTo.HasValue || instance.CreationDate <= request.DateTo.Value)
            && (request.ProcessNames == null || request.ProcessNames!.Count == 0 || request.ProcessNames.Contains(instance.Process.Name));

            var dbResult = await _queryRepo.GetListAsync<ProcessInstance>(condition: condition,
                includes: source => source.Include(s => s.CurrentActivity),
                asNoTracking: true);

            var workList = _mapper.Map<List<Worklist>>(dbResult);
            return new Result<List<Worklist>>(workList, true);
        }
    }
}
