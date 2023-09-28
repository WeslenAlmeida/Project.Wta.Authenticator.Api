using CrossCutting.Configuration;

namespace Domain.Security
{
    public class TokenConfiguration
    {
        public string? Audience { get; set; } = AppSettings.TokenConfiguration.Audience;
        public string? Issuer { get; set; } = AppSettings.TokenConfiguration.Issuer;
        public int Seconds { get; set; } = int.Parse(AppSettings.TokenConfiguration.Seconds!);
    }
}