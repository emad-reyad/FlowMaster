using AutoMapper;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserAvailableWorklist
{
    internal class GetUserAvailableWorklistMapper : Profile
    {
        public GetUserAvailableWorklistMapper()
        {
            CreateMap<ProcessInstance, Worklist>();
        }
    }
}
