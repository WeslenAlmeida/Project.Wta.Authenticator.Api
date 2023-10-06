using System.Net;
using Domain.Commands.v1.GenerateToken;
using Domain.Queries.v1.GetToken;
using Domain.Shared.v1.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.v1
{
    [Route("api/v1/token")]
    public class TokenController : ApiBaseController
    {
        public TokenController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody]GenerateTokenCommand request)
        {
            return await GenerateHttpResponse(request, HttpStatusCode.Created);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateToken([FromBody]GetTokenQuery request)
        {
            return await GenerateHttpResponse(request, HttpStatusCode.OK);
        }
    }
}