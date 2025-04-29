using AutoMapper;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserTaskInfo
{
    public class GetUserTaskInfoMapper : Profile
    {
        public GetUserTaskInfoMapper()
        {
            CreateMap<ProcessInstance, GetUserTaskInfoResponse>();
        }
    }
}
