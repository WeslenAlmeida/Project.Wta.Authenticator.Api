using CrossCutting.Configuration.AppModels;

namespace CrossCutting.Configuration
{
    public static class AppSettings
    {
        public static MongoSettings MongoSettings { get{ return new MongoSettings(); } }
        public static TokenConfiguration TokenConfiguration { get{ return new TokenConfiguration(); } }
        public static RedisSettings RedisSettings { get{ return new RedisSettings(); } }
        public static ApiKey ApiKey { get{ return new ApiKey(); } }
        public static DomainsAllowed DomainsAllowed { get{ return new DomainsAllowed(); } }
    }
}