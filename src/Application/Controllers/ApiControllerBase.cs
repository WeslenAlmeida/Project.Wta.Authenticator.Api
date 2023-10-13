using System.Net;
using CrossCutting.Configuration;
using CrossCutting.Exception;
using Domain.Shared.v1.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.Controllers
{
    [ApiKey]   
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly ISender _mediator;
        private readonly ILogger<ApiBaseController> _logger;

        public ApiBaseController(ISender mediator, ILogger<ApiBaseController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        protected async Task<IActionResult> GenerateHttpResponse(object request, HttpStatusCode statusCode)
        {
            try
            {
                _logger.LogInformation("Start request Api");
                var response = await _mediator.Send(request);
                return StatusCode((int)statusCode, response);
            }
            catch (BaseException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.CustomMessage);
            }
            catch(Exception ex)
            {
                _logger.LogError(JsonConvert.SerializeObject(ex));
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}