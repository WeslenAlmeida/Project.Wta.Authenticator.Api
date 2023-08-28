using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Commands.v1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.Controllers.v1
{
    [Route("api/v1/user")]
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