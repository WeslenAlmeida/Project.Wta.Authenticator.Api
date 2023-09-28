using System.Net;
using Domain.Commands.v1.GenerateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers.v1
{
    [Route("api/v1/token")]
    public class UserController : ApiBaseController
    {
        public UserController(IMediator mediator) : base(mediator)
        {
        }
        
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody]GenerateTokenCommand request)
        {
            return await GenerateHttpResponse(request, HttpStatusCode.Created);
        }
    }
}