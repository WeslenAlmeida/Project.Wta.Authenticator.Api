using System.Net;
using CrossCutting.Exception;
using Domain.Shared.v1.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiKey]   
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApiBaseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected async Task<IActionResult> GenerateHttpResponse(object request, HttpStatusCode statusCode)
        {
            try
            {
               var response = await _mediator.Send(request);
               return StatusCode((int)statusCode, response);
               
            }
            catch (BaseException ex)
            {
                return StatusCode(ex.StatusCode, ex.CustomMessage);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}