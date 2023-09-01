namespace CrossCutting.Configuration.AppModels
{
    public class ValidationId
    {
        public string? Id 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["ValidationId"];
            }
        }
    }
}
        
   
