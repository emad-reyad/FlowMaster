using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace WorkFlowEngine.Api.Filters
{
    public class ErrorHandlingFilter:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception=context.Exception;
            context.ExceptionHandled=true;
            var problemDetails = new ProblemDetails
            { 
                Title = "An error occurred while processing the request",
                Status=(int)HttpStatusCode.InternalServerError,
                
            };
            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true; 
        }
    }
}
