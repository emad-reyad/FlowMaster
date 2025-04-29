

using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkFlowEngine.Application.Features.Authentication.Queries.Login;

namespace WorkFlowEngine.Api.Controllers
{
    [Route(template: RoutTable.AuthenticationManager.Controller)]
    [ApiController]
    public class AuthenticationController : BaseApiController
    {

        public AuthenticationController(ISender sender) : base(sender)
        { }

        [HttpPost(template: RoutTable.AuthenticationManager.Login)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _mediator.Send(request);
            return ResturnResult<LoginsResponse>(result);
        }
    }
}
