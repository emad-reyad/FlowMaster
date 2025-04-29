using AutoMapper;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance
{
    public class StartProcessInstanceMapper : Profile
    {
        public StartProcessInstanceMapper()
        {
            CreateMap<ProcessInstance, StartProcessInstanceResponse>().ReverseMap();
            //CreateMap<Activity, CurrentActivity>().ReverseMap();
        }
    }
}
