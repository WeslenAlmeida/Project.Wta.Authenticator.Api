using System.Net;
using Domain.Commands.v1.GenerateToken;
using Domain.Queries.v1.GetToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.v1
{
    [Route("api/v1/token")]
    public class TokenController : ApiBaseController
    {
        public TokenController(ISender mediator, ILogger<ApiBaseController> logger) : base(mediator, logger)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody]GenerateTokenCommand request)
        {
            return await GenerateHttpResponse(request, HttpStatusCode.OK);
        }

        [HttpGet]
        public async Task<IActionResult> GetToken([FromBody]GetTokenInfoQuery request)
        {
            return await GenerateHttpResponse(request, HttpStatusCode.OK);
        }
    }
}