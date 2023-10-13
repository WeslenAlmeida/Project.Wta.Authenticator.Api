using CrossCutting.Exception.CustomExceptions;
using Domain.Interfaces.v1;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Queries.v1.GetToken
{
    public class GetTokenInfoQueryHandler : IRequestHandler<GetTokenInfoQuery, GetTokenInfoQueryResponse>
    {
        private IRedisService _redis;
        private readonly ILogger<GetTokenInfoQueryHandler> _logger;
        public GetTokenInfoQueryHandler(IRedisService redis, ILogger<GetTokenInfoQueryHandler> logger)
        {
            _redis = redis;
            _logger = logger;
        }

        public async Task<GetTokenInfoQueryResponse> Handle(GetTokenInfoQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GetTokenInfoQueryHandler");

            var token = await _redis.GetAsync(request.Token!);

            if(string.IsNullOrEmpty(token)) throw new TokenNotFoundException();

            var response = JsonConvert.DeserializeObject<GetTokenInfoQueryResponse>(token)!;

            response.Email = ReplaceEmail(response.Email!);

            _logger.LogInformation("End GetTokenInfoQueryHandler");

            return response;
        }

        private string ReplaceEmail(string email)
        {
            var array = email.ToCharArray();

            for(int i = 3; i < array.Length -3 ; i ++ )
            {
                array[i] = '*';
            }

            return new string(array);
        }
    }
}