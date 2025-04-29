using System.Net;
using System.Text.Json;

namespace WorkFlowEngine.Api.MiddleWare
{
    public class ErrorHandlingMiddleWare
    {
        private const string _ContactErrorMessage = "Error occurred, Contact administrator";
        private readonly RequestDelegate _requestDelegate;

        public ErrorHandlingMiddleWare(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        } 
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) { 
            var code=HttpStatusCode .InternalServerError;
            var result = JsonSerializer.Serialize(new { error = _ContactErrorMessage });
            context.Response.ContentType= "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsJsonAsync(result);
        }
        
    }
}
