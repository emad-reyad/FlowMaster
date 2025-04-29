using AutoMapper;
using WorkFlowEngine.Domain.Entities;

namespace WorkFlowEngine.Application.Features.ProcessManager.Queries.GetCurrentActivity
{
    public class GetCurrentActivityMapper : Profile
    {
        public GetCurrentActivityMapper()
        {
            CreateMap<Activity, GetCurrentActivityResponse>();
        }
    }
}
