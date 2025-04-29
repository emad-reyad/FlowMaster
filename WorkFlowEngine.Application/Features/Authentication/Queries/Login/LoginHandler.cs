using MediatR;
using Microsoft.Extensions.Options;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared;
using WorkFlowEngine.Shared.Settings;

namespace WorkFlowEngine.Application.Features.Authentication.Queries.Login
{
    public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginsResponse>>
    {
        private readonly IJwtService _jwtService;
        private readonly ClientSettings _clientSettings;

        public LoginHandler(IJwtService jwtService, IOptions<ClientSettings> clientSettings)
        {
            _jwtService = jwtService;
            _clientSettings = clientSettings.Value;
        }

        public async Task<Result<LoginsResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            //check in database if exist or not
            Client client = null!;
            if (_clientSettings.Clients != null)
            {
                client = _clientSettings.Clients.FirstOrDefault(s => s.Username == request.Username && s.Password == request.Password)!;
            }
            if (client == null)
                return new Result<LoginsResponse>(false, new List<KeyValuePair<string, string>>{
                    new KeyValuePair<string, string> (nameof(Constants.Error.InvalidUsernameOrPassword),Constants.Error.InvalidUsernameOrPassword)
                });
            //generate token
            var token = _jwtService.GenerateToken(client.Id, client.Name);
            return await Task.Run(() =>
                new Result<LoginsResponse>(new LoginsResponse(token), true));
        }
    }
}
