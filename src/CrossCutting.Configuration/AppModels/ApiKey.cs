namespace CrossCutting.Configuration.AppModels
{
    public class ApiKey
    {
        public string? Id 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["ApiKey"];
            }
        }
    }
}
        
   
