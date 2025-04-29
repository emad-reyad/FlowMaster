using MediatR;
using WorkFlowEngine.Shared;

namespace WorkFlowEngine.Application.Features.Authentication.Queries.Login
{
    public record LoginRequest(string Username, string Password) : IRequest<Result<LoginsResponse>>;
}
