namespace Domain.Interfaces.v1
{
    public interface IRedisService
    {
        Task SetAsync(Guid key, string value);
        Task<string> GetAsync(string key);
    }
}