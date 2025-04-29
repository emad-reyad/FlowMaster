using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserOpenedWorklist
{
    internal class GetUserOpenedWorklistHandler : BaseQueryHandler, IRequestHandler<GetUserOpenedWorklistRequest, Result<List<Worklist>>>
    {
        public GetUserOpenedWorklistHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
        }

        public async Task<Result<List<Worklist>>> Handle(GetUserOpenedWorklistRequest request, CancellationToken cancellationToken)
        {

            var dbResult = await _queryRepo.GetListAsync<ProcessInstance>(
                condition: s =>
                    s.IsActive &&
                    s.Opened &&
                    s.LastModifiedBy == request.UserId,
                includes: s => s.Include(s => s.CurrentActivity),
                asNoTracking: true);

            var workList = _mapper.Map<List<Worklist>>(dbResult);
            return new Result<List<Worklist>>(workList, true);
        }
    }
}
