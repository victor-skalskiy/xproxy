using XProxy.Interfaces;

namespace XProxy.Services;

public sealed class ExchangeServiceFactory : IExchangeServiceFactory
{
    private readonly IXProxyOptions _xProxyOptions;
    private readonly IUserSettingsStorage _userSettingsStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public ExchangeServiceFactory(
        IXProxyOptions xProxyOptions,
        IUserSettingsStorage userSettingsStorage,
        IHttpClientFactory httpClientFactory)
    {
        _xProxyOptions = xProxyOptions;
        _userSettingsStorage = userSettingsStorage;
        _httpClientFactory = httpClientFactory;
    }

    #region Implementation of IExchangeServiceFactory

    public Task<IExchangeService> CreateAsync(long userSettingsId, CancellationToken cancellationToken = default)
    {
        return InternalCreate(userSettingsId, cancellationToken);
    }

    public Task<IExchangeService> CreateDefaultAsync(CancellationToken cancellationToken = default)
    {
        return InternalCreate(_xProxyOptions.DefaultUserSettingsId, cancellationToken);
    }

    #endregion

    private async Task<IExchangeService> InternalCreate(
        long userSettingsId,
        CancellationToken cancellationToken = default)
    {
        if (!_xProxyOptions.UpLink)
        {
            return new NopExchangeService();
        }

        return new ExchangeService(
            await _userSettingsStorage.GetUserSettingsAsync(userSettingsId, cancellationToken),
            new ExchangeServiceOptions(
                _xProxyOptions.AV100DictionaryAPIOperation,
                _xProxyOptions.AV100RegionAPIParameters,
                _xProxyOptions.AV100SourceAPIParameters),
            _httpClientFactory, _xProxyOptions);

    }
}