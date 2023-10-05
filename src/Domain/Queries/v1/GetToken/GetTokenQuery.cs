using MediatR;

namespace Domain.Queries.v1.GetToken
{
    public class GetTokenQuery : IRequest<GetTokenQueryResponse>
    {
        public string? Token { get; set; }
    }
}