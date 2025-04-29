using AutoMapper;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManager.Command.ExecuteActivityAction
{
    public class ExecuteActivityActionMapper : Profile
    {
        public ExecuteActivityActionMapper()
        {
            CreateMap<ProcessInstance, ExecuteActivityActionResponse>().ReverseMap();
            CreateMap<Activity, CurrentActivity>().ReverseMap();
        }
    }
}
