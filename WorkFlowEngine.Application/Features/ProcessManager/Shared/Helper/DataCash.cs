using Microsoft.EntityFrameworkCore;
using WorkFlowEngine.Application.Abstractions.ProcessManager;
using WorkFlowEngine.Domain.Entities;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;
using WorkFlowEngine.Shared.Cashing;

namespace WorkFlowEngine.Application.Features.ProcessManager.Shared.Helper
{
    internal class DataCash : IDataCash
    {
        private readonly IAdHelpper _adHelper;
        private readonly ICashService _cashService;
        private readonly IProcessMangerQueryRepository _queryRepo;



        public DataCash(IAdHelpper helper, ICashService cashService, IProcessMangerQueryRepository queryRepo)
        {
            _adHelper = helper;
            _cashService = cashService;
            _queryRepo = queryRepo;
        }

        public async Task<Process> GetProcessByName(string processName)
        {
            var cashKey = string.Format(Constants.CashKeys.Process, processName);
            Process process = _cashService.GetCash<Process>(cashKey);
            if (process is null)
            {
                process = await _queryRepo.GetAsync<Process>(
                    condition: s => s.Name == processName,
                    includes: source => source.AsSplitQuery().
                        Include(s => s.ProcessActivities).
                            ThenInclude(s => s.Activity).
                                ThenInclude(s => s.ActivityDestinationTypes).
                                    ThenInclude(s => s.DestinationType).
                        Include(s => s.ProcessActivities).
                            ThenInclude(s => s.Activity).
                                      ThenInclude(s => s.ActivityActions).
                                        ThenInclude(s => s.Action),
                    asNoTracking: false);
                if (process is null)
                    return process!;

                process.ProcessDataFields = await _queryRepo.GetListAsync<ProcessDataFields>(
                    condition: s => s.ProcessId == process.Id,
                    includes: source => source.
                            Include(s => s.DataField),
                    asNoTracking: false);

                process.Transitions = await _queryRepo.GetListAsync<Transition>(
                    condition: s => s.ProcessId == process.Id,
                    includes: source => source.
                            Include(s => s.TransitionConditions),
                    asNoTracking: false);
                _cashService.SetCash(cashKey, process, DateTimeOffset.Now.AddMinutes(Constants.CashExpirationInMinutes.Default));
            }
            return process;
        }
        public async Task<Process> GetProcessById(int processId)
        {
            var cashKey = string.Format(Constants.CashKeys.Process, processId);
            Process process = _cashService.GetCash<Process>(cashKey);
            if (process is null)
            {
                process = await _queryRepo.GetAsync<Process>(
                    condition: s => s.Id == processId,
                    includes: source => source.
                        Include(s => s.ProcessActivities).
                            ThenInclude(s => s.Activity).
                                ThenInclude(s => s.ActivityDestinationTypes).
                                    ThenInclude(s => s.DestinationType).
                        Include(s => s.ProcessActivities).
                            ThenInclude(s => s.Activity).
                                      ThenInclude(s => s.ActivityActions).
                                        ThenInclude(s => s.Action),
                    asNoTracking: false);
                if (process is null)
                    return process!;

                process.ProcessDataFields = await _queryRepo.GetListAsync<ProcessDataFields>(
                    condition: s => s.ProcessId == process.Id,
                    includes: source => source.
                            Include(s => s.DataField),
                    asNoTracking: false);

                process.Transitions = await _queryRepo.GetListAsync<Transition>(
                    condition: s => s.ProcessId == process.Id,
                    includes: source => source.
                            Include(s => s.TransitionConditions),
                    asNoTracking: false);
                _cashService.SetCash(cashKey, process, DateTimeOffset.Now.AddMinutes(Constants.CashExpirationInMinutes.Default));
            }
            return process;
        }

        public List<string> GetUserGroups(string userId)
        {
            List<string> userGroups;

            if (userId.Contains("\\"))
            {
                var userGroupCashKey = string.Format(Constants.CashKeys.UserGroups, userId);
                userGroups = _cashService.GetCash<List<string>>(userGroupCashKey);
                if (userGroups is null || userGroups.Count == 0)
                {
                    userGroups = _adHelper.GetUserGroups(userId);
                    _cashService.SetCash(userGroupCashKey, userGroups, DateTime.Now.AddMinutes(Constants.CashExpirationInMinutes.Default));
                }
            }
            else
                userGroups = new List<string> { userId };
            return userGroups;
        }

    }
}
