namespace CrossCutting.Configuration.AppModels
{
    public class ApiKey
    {
        public string? Name 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["Authorization:ApiKeyName"];
            }
        }

         public string? Key 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["Authorization:ApiKey"];
            }
        }
    }
}
        
   
