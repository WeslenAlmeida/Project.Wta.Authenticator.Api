using CrossCutting.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Domain.Shared.v1.Authorization
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private string ApiKeyName = AppSettings.ApiKey.Name!;
        private string ApiKey = AppSettings.ApiKey.Key!;

        public async Task OnActionExecutionAsync( ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyName, out var extractedApiKey) || !ApiKey.Equals(extractedApiKey))
            {
                var exception = new CrossCutting.Exception.CustomExceptions.UnauthorizedException();
                context.Result = new ContentResult()
                {
                    StatusCode = exception.StatusCode,
                    Content = exception.CustomMessage!.ToString()
                };
                return; 
            }

            await next();
        }
    }
}