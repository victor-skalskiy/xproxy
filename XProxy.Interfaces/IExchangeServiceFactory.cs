namespace XProxy.Interfaces;

public interface IExchangeServiceFactory
{
    Task<IExchangeService> CreateAsync(long userSettingsId, CancellationToken cancellationToken = default);
    
    Task<IExchangeService> CreateDefaultAsync(CancellationToken cancellationToken = default);
}