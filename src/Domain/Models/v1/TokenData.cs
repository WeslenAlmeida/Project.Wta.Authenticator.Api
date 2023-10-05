namespace Domain.Models.v1
{
    public class TokenData
    {
        public string? Email { get; set; }
        public DateTime  ExpirateDate { get; set; }

        public TokenData(string email, DateTime expirateDate) 
        {
            Email = email;
            ExpirateDate = expirateDate;
        }
    }
}