namespace Domain.Queries.v1.GetToken
{
    public class GetTokenInfoQueryResponse
    {
        public string? Email { get; set; }
        public string  ExpirateDate { get; set; }

        public GetTokenInfoQueryResponse(string email, DateTime expirateDate) 
        {
            Email = email;
            ExpirateDate = expirateDate.AddHours(-3).ToString("dd/MM/yyyy hh:mm:ss");
        }
    }
}