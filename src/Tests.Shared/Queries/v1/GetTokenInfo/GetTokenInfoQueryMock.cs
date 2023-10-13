using Domain.Queries.v1.GetToken;

namespace Tests.Shared.Queries.v1.GetTokenInfo
{
    public class GetTokenInfoQueryMock
    {
         public static GetTokenInfoQuery GetDefault() =>
            new GetTokenInfoQuery
            {
                Token = "teste",
            };

        public static GetTokenInfoQuery GetFailed() =>
            new GetTokenInfoQuery
            {
                Token = null,
            };
    }
}