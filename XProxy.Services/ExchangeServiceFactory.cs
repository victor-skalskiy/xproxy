using Newtonsoft.Json.Linq;
using XProxy.DAL;
using XProxy.Interfaces;

namespace XProxy.Services;

public sealed class ExchangeServiceFactory : IExchangeServiceFactory
{
    private readonly IXProxyOptions _xProxyOptions;
    private readonly IUserSettingsStorage _userSettingsStorage;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IFilterStorage _filterStorage;
    private readonly DataContext _context;
    private readonly ITelegramBotService _telegramBotService;

    public ExchangeServiceFactory(
        IXProxyOptions xProxyOptions,
        IUserSettingsStorage userSettingsStorage,
        IHttpClientFactory httpClientFactory,
        IFilterStorage filterStorage,
        ITelegramBotService telegramBotService,
        DataContext context)
    {
        _xProxyOptions = xProxyOptions;
        _userSettingsStorage = userSettingsStorage;
        _httpClientFactory = httpClientFactory;
        _filterStorage = filterStorage;
        _context = context;
        _telegramBotService = telegramBotService;
    }

    #region Implementation of IExchangeServiceFactory

    public Task<IExchangeService> CreateAsync(long userSettingsId, long av100filterId, CancellationToken cancellationToken = default)
    {
        return InternalCreate(userSettingsId, av100filterId, cancellationToken);
    }

    public Task<IExchangeService> CreateDefaultAsync(CancellationToken cancellationToken = default)
    {
        return InternalCreate(_xProxyOptions.DefaultUserSettingsId, _xProxyOptions.DefaultFilterId, cancellationToken);
    }

    #endregion

    private async Task<IExchangeService> InternalCreate(
        long userSettingsId,
        long av100filterId,
        CancellationToken token = default)
    {
        if (!_xProxyOptions.UpLink)
        {
            return new NopExchangeService();
        }

        return new ExchangeService(
            await _userSettingsStorage.GetUserSettingsAsync(userSettingsId, token),
            new ExchangeServiceOptions(
                _xProxyOptions.AV100DictionaryAPIOperation,
                _xProxyOptions.AV100RegionAPIParameters,
                _xProxyOptions.AV100SourceAPIParameters),
        _httpClientFactory,
            await _filterStorage.GetFilterAsync(av100filterId, token),
            _xProxyOptions,
            _telegramBotService,
            _context);
    }
}