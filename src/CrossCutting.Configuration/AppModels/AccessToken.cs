namespace CrossCutting.Configuration.AppModels
{
    public class AccessToken
    {
        public string? Id 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["AccessToken"];
            }
        }
    }
}
        
   
