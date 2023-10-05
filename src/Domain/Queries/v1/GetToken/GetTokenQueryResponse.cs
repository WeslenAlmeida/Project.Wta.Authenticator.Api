namespace Domain.Queries.v1.GetToken
{
    public class GetTokenQueryResponse
    {
        public string? Email { get; set; }
        public DateTime  ExpirateDate { get; set; }

        public GetTokenQueryResponse(string email, DateTime expirateDate) 
        {
            Email = email;
            ExpirateDate = expirateDate;
        }
    }
}