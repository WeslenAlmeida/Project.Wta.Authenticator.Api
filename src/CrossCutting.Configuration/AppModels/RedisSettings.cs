namespace CrossCutting.Configuration.AppModels
{
    public class RedisSettings
    {
        public string? ConnectionString 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["RedisSettings:ConnectionString"];
            }
        }

         public string? TimeExpiration 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["RedisSettings:TimeExpiration"];
            }
        }
    }
}