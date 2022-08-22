namespace XProxy.Interfaces;

public interface IAV100ExchangeService
{
    /// <summary>
    /// Get counts of expected result values
    /// </summary>
    Task<long> AV100ReuestListCount(long fromId, long toId, CancellationToken token = default);


    Task<string> AV100RequestString(CancellationToken token = default);
}