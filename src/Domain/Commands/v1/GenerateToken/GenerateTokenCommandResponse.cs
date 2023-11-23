namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommandResponse
    {
        public bool Authenticated { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
    }
}