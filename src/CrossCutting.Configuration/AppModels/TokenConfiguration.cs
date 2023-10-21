namespace CrossCutting.Configuration.AppModels
{
    public class TokenConfiguration
    {
         public string? Audience 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["TokenConfiguration:Audience"];
            }
        }

        public string? Issuer 
         { 
            get
            {
                return ConfigurationAppSettings.Builder()["TokenConfiguration:Issuer"];
            }
        }

         public string? Key 
         { 
            get
            {
                return ConfigurationAppSettings.Builder()["TokenConfiguration:Key"];
            }
        }

         public string? Seconds 
         { 
            get
            {
                return ConfigurationAppSettings.Builder()["TokenConfiguration:Seconds"];
            }
        }
    }
}