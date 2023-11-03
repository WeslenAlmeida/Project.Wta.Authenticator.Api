namespace CrossCutting.Configuration.AppModels
{
    public class DomainsAllowed
    {
         public string? Domains 
        { 
            get
            {
                return ConfigurationAppSettings.Builder()["DomainsAllowed"];
            }
        }
    }
}