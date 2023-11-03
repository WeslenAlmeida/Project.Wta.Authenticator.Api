using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Domain.Shared.v1.Authorization
{
    public class RestrictDomainAttribute : Attribute, IAuthorizationFilter
    {
        public IEnumerable<string> AllowedHosts { get; }
        public int? Port { get; }

        public RestrictDomainAttribute(params string[] allowedHosts) => AllowedHosts = allowedHosts;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string host = context.HttpContext.Request.Host.Host;

            var port = context.HttpContext.Request.Host.Port;

            if (!AllowedHosts.Contains(host, StringComparer.OrdinalIgnoreCase))
            {
                context.Result = new BadRequestObjectResult($"Host {host} is not allowed");
            }
        }
    }
}