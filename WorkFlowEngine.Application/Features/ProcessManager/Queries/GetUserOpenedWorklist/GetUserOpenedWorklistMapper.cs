using AutoMapper;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserOpenedWorklist
{
    internal class GetUserOpenedWorklistMapper : Profile
    {
        public GetUserOpenedWorklistMapper()
        {
            CreateMap<ProcessInstance, Worklist>();
        }
    }
}
