using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkFlowEngine.Application.Features.ProcessManager.Command.CancelProcessInstance;
using WorkFlowEngine.Application.Features.ProcessManager.Command.ExecuteActivityAction;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetActivityActions;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetCurrentActivity;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetSerialNumber;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserAvailableWorklist;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserOpenedWorklist;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserTaskInfo;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.GetUserWorklistStatistics;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.IsTaskAvailable;
using WorkFlowEngine.Application.Features.ProcessManager.Queries.IsWorkItemAvailable;
using WorkFlowEngine.Application.Features.ProcessManager.Shared.Models;
using WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.OpenTask;
using WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.ReleaseTask;
using WorkFlowEngine.Application.Features.ProcessManagerFeatures.Command.StartProcessInstance;

namespace WorkFlowEngine.Api.Controllers
{
    [Authorize]
    [Route(template: RoutTable.ProcessManager.Controller)]
    [ApiController]
    public class ProcessManagerController : BaseApiController
    {
        public ProcessManagerController(ISender mediator) : base(mediator)
        {
        }

        [HttpPost(template: RoutTable.ProcessManager.StartProcessInstance)]

        public async Task<IActionResult> StartProcessInstance(StartProcessInstanceRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<StartProcessInstanceResponse>(result);
        }
        [HttpPost(template: RoutTable.ProcessManager.ExecuteActivityAction)]
        public async Task<IActionResult> ExecuteActivityAction(ExecuteActivityActionRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<ExecuteActivityActionResponse>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.GetActivityActionNames)]
        public async Task<IActionResult> GetActivityActionNames([FromQuery] GetActivityActionsRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<List<string>>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.GetCurrentActivity)]
        public async Task<IActionResult> GetCurrentActivity([FromQuery] GetCurrentActivityRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<GetCurrentActivityResponse>(result);
        }

        [HttpPost(template: RoutTable.ProcessManager.GetUserAvailableWorklist)]
        public async Task<IActionResult> GetUserAvailableWorklist(GetUserAvailableWorklistRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<List<Worklist>>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.GetUserOpenedWorklist)]
        public async Task<IActionResult> GetUserOpenedWorklist([FromQuery] GetUserOpenedWorklistRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<List<Worklist>>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.IsWorkItemAvailable)]
        public async Task<IActionResult> IsWorkItemAvailable([FromQuery] IsWorkItemAvailableRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<bool>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.IsTaskAvailable)]
        public async Task<IActionResult> IsTaskAvailable([FromQuery] IsTaskAvailableRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<int>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.GetUserWorklist)]
        public async Task<IActionResult> GetUserWorklist([FromQuery] GetUserWorklistRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<List<GetUserWorklistResponse>>(result);

        }

        [HttpPost(template: RoutTable.ProcessManager.CancelProcessInstance)]
        public async Task<IActionResult> CancelProcessInstance(CancelProcessInstanceRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<bool>(result);
        }
        [HttpPost(template: RoutTable.ProcessManager.OpenTask)]
        public async Task<IActionResult> OpenTask(OpenTaskRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<bool>(result);
        }
        [HttpPost(template: RoutTable.ProcessManager.ReleaseTask)]
        public async Task<IActionResult> ReleaseTask(ReleaseTaskRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<bool>(result);
        }

        [HttpGet(template: RoutTable.ProcessManager.GetUserTaskInfo)]
        public async Task<IActionResult> GetUserTaskInfo([FromQuery] GetUserTaskInfoRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<GetUserTaskInfoResponse>(result);
        }
        [HttpGet(template: RoutTable.ProcessManager.GetSerialNumber)]
        public async Task<IActionResult> GetSerialNumber([FromQuery] GetSerialNumberRequest request)
        {
            var result = await _mediator.Send(request);
            return ReturnResult<int>(result);
        }

    }
}
