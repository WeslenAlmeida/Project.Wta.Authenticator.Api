namespace Domain.Interfaces.v1
{
    public interface IRedisService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}