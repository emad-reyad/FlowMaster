using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected readonly ISender _mediator;


        public BaseApiController(ISender mediator)
        {
            _mediator = mediator;
        }
        public IActionResult ReturnResult<T>(Result<T> data)
        {
            if (data == null)
                return BadRequest(Constants.Error.NoResponse);
            else if (!data.IsSuccess)
                return Problem(
                    statusCode: 400,
                    detail: string.Join(".\n", data.Errors.Select(s => $"{s.Key}:{s.Value}")),
                    title: "One or more validation errors occurred.");
            else
                return Ok(data);
        }
    }
}
