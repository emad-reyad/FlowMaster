using AutoMapper;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserWorklistStatistics;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserWorklist
{
    public class GetUserWorklistMapper : Profile
    {
        public GetUserWorklistMapper()
        {
            CreateMap<ProcessInstance, GetUserWorklistResponse>();
        }
    }
}
