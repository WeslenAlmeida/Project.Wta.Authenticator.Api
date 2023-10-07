using CrossCutting.Exception.CustomExceptions;
using Domain.Interfaces.v1;
using MediatR;
using Newtonsoft.Json;

namespace Domain.Queries.v1.GetToken
{
    public class GetTokenInfoQueryHandler : IRequestHandler<GetTokenInfoQuery, GetTokenInfoQueryResponse>
    {
        private IRedisService _redis;
        public GetTokenInfoQueryHandler(IRedisService redis)
        {
            _redis = redis;
        }

        public async Task<GetTokenInfoQueryResponse> Handle(GetTokenInfoQuery request, CancellationToken cancellationToken)
        {
            var token = await _redis.GetAsync(request.Token!);

            if(string.IsNullOrEmpty(token)) throw new TokenNotFoundException();

            return JsonConvert.DeserializeObject<GetTokenInfoQueryResponse>(token)!;
        }
    }
}