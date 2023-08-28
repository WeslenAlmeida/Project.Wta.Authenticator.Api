namespace CrossCutting.Configuration.AppModels
{
    public class MongoSettings
    {
        public string? ConnectionString 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["MongoSettings:ConnectionString"];
            }
        }

        public string? DatabaseName 
         { 
            get
            {
                return ConfigurationAppSettings.Builder()["MongoSettings:DatabaseName"];
            }
        }
    }
}