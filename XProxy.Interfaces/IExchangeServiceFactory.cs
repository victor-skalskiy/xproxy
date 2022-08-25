namespace XProxy.Interfaces;

public interface IExchangeServiceFactory
{
    Task<IExchangeService> CreateAsync(long userSettingsId, long av100filterId, CancellationToken cancellationToken = default);
    
    Task<IExchangeService> CreateDefaultAsync(CancellationToken cancellationToken = default);
}