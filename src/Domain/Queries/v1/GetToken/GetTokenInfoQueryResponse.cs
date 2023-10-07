namespace Domain.Queries.v1.GetToken
{
    public class GetTokenInfoQueryResponse
    {
        public string? Email { get; set; }
        public DateTime  ExpirateDate { get; set; }

        public GetTokenInfoQueryResponse(string email, DateTime expirateDate) 
        {
            Email = email;
            ExpirateDate = expirateDate;
        }
    }
}