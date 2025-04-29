using AutoMapper;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Infrastructure.Abstraction;

namespace WorkFlowEngine.Application.Features.ProcessManager
{
    public abstract class BaseQueryHandler
    {
        protected readonly IProcessMangerQueryRepository _queryRepo;
        protected readonly IMapper _mapper;
        protected readonly IDataCash _dataCash;
        protected readonly IInstanceAccess _instanceAccess;
        protected BaseQueryHandler(IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess)
        {
            _mapper = mapper;
            _queryRepo = queryRepo;
            _dataCash = dataCash;
            _instanceAccess = instanceAccess;
        }
    }
    public abstract class BaseCommandHandler : BaseQueryHandler
    {

        protected readonly IProcessMangerCommandReposistory _commandRepo;
        public BaseCommandHandler(IProcessMangerCommandReposistory commandRepo, IMapper mapper, IProcessMangerQueryRepository queryRepo, IDataCash dataCash, IInstanceAccess instanceAccess) : base(mapper, queryRepo, dataCash, instanceAccess)
        {
            _commandRepo = commandRepo;
        }
    }
}
