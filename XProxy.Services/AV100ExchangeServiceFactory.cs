using XProxy.Interfaces;

namespace XProxy.Services;

public class AV100ExchangeServiceFactory : IAV100ExchangeServiceFactory
{
    private readonly IXProxyOptions _xProxyOptions;
    private readonly IUserSettingsStorage _userSettingsStorage;
    private readonly IFilterStorage _filterStorage;
    private readonly IHttpClientFactory _httpClientFactory;

    public AV100ExchangeServiceFactory(
        IXProxyOptions xProxyOptions,
        IUserSettingsStorage userSettingsStorage,
        IFilterStorage filterStorage,
        IHttpClientFactory httpClientFactory)
    {
        _xProxyOptions = xProxyOptions;
        _userSettingsStorage = userSettingsStorage;
        _filterStorage = filterStorage;
        _httpClientFactory = httpClientFactory;
    }

    public Task<IAV100ExchangeService> CreateAsync(long userSettingsId, long av100filterId, CancellationToken token = default)
    {
        return InternalCreate(userSettingsId, av100filterId, token);
    }

    public Task<IAV100ExchangeService> CreateDefaultAsync(CancellationToken token = default)
    {
        return InternalCreate(_xProxyOptions.DefaultUserSettingsId, _xProxyOptions.DefaultFilterId, token);
    }

    private async Task<IAV100ExchangeService> InternalCreate(
        long userSettingsId,
        long filterId,
        CancellationToken token = default)
    {
        return new AV100ExchangeService(
            new ExchangeServiceOptions(
                _xProxyOptions.AV100DictionaryAPIOperation,
                _xProxyOptions.AV100RegionAPIParameters,
                _xProxyOptions.AV100SourceAPIParameters),
            await _userSettingsStorage.GetUserSettingsAsync(userSettingsId, token),
            await _filterStorage.GetFilterAsync(filterId, token),
            _httpClientFactory,
            _xProxyOptions);
    }
}