namespace XProxy.Interfaces;

public interface IAV100ExchangeServiceFactory
{
    Task<IAV100ExchangeService> CreateAsync(long userSettingsId, long av100filterId, CancellationToken token = default);

    Task<IAV100ExchangeService> CreateDefaultAsync(CancellationToken token = default);
}