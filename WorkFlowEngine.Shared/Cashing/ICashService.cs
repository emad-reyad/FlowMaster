namespace WorkFlowEngine.Shared.Cashing
{
    public interface ICashService
    {
        T GetCash<T>(string key);
        bool SetCash<T>(string key, T value, DateTimeOffset expirationDate);
        bool RemoveCash(string key);
    }
}
