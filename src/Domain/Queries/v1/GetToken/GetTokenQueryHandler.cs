using CrossCutting.Exception.CustomExceptions;
using Domain.Interfaces.v1;
using MediatR;
using Newtonsoft.Json;

namespace Domain.Queries.v1.GetToken
{
    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, GetTokenQueryResponse>
    {
        private IRedisService _redis;
        public GetTokenQueryHandler(IRedisService redis)
        {
            _redis = redis;
        }

        public async Task<GetTokenQueryResponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var token = await _redis.GetAsync(request.Token!) ?? throw new TokenNotFoundException();

            return JsonConvert.DeserializeObject<GetTokenQueryResponse>(token)!;
        }
    }
}