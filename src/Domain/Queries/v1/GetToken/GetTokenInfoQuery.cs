using MediatR;

namespace Domain.Queries.v1.GetToken
{
    public class GetTokenInfoQuery : IRequest<GetTokenInfoQueryResponse>
    {
        public string? Token { get; set; }
    }
}