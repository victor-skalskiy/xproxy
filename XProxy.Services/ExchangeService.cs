using XProxy.Interfaces;
using XProxy.Domain;
using Newtonsoft.Json;
using System.Text;
using XProxy.DAL;
using Microsoft.EntityFrameworkCore;

namespace XProxy.Services;

public sealed class ExchangeService : IExchangeService
{
    private const string JsonContentType = "application/json";
    private readonly UserSettings _userSettings;
    private readonly ExchangeServiceOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IXProxyOptions _xOptions;
    private readonly AV100Filter _filter;
    private readonly DataContext _context;
    private readonly ITelegramBotService _telegramBotService;

    public ExchangeService(
        UserSettings userSettings,
        ExchangeServiceOptions options,
        IHttpClientFactory httpClientFactory,
        AV100Filter filter,
        IXProxyOptions xProxyOptions,
        ITelegramBotService telegramBotService,
        DataContext context)
    {
        _userSettings = userSettings;
        _options = options;
        _httpClientFactory = httpClientFactory;
        _xOptions = xProxyOptions;
        _filter = filter;
        _context = context;
        _telegramBotService = telegramBotService;
    }

    #region private helper methods

    private static StringContent CreateContent(string content)
    {
        return new StringContent(content ?? string.Empty, Encoding.UTF8, JsonContentType);
    }

    private static StringBuilder ParamsBuilder(string baseUrl = default, long yearStart = default, long yearEnd = default,
            long priceStart = default, long priceEnd = default, long distanceStart = default, long distanceEnd = default,
            long[] regions = default, long[] sources = default, long fromId = default, long toId = default)
    {
        var sb = new StringBuilder(baseUrl);

        if (yearStart != 0)
            sb.Append($"&yearStart={yearStart}");

        if (yearEnd != 0)
            sb.Append($"&yearEnd={yearEnd}");

        if (priceStart != 0)
            sb.Append($"&priceStart={priceStart}");

        if (priceEnd != 0)
            sb.Append($"&priceEnd={priceEnd}");

        if (distanceStart != 0)
            sb.Append($"&distanceStart={distanceStart}");

        if (distanceEnd != 0)
            sb.Append($"&distanceEnd={distanceEnd}");

        if (regions.Length > 0)
            sb.Append($"&listregionid=.{string.Join(".", regions)}.");

        if (sources.Length > 0)
            sb.Append($"&source=.{string.Join(".", sources)}.");

        if (fromId != 0)
            sb.Append($"&fromId={fromId}");

        if (toId != 0)
            sb.Append($"&toId={toId}");

        return sb;
    }

    /// <summary>
    /// Cleaninng some titles in regions
    /// </summary>
    private static string CleanRegionName(string durtyName)
    {
        var result = durtyName;
        if (result.Contains('('))
            result = result.Substring(0, durtyName.IndexOf('('));

        result = result.Replace("Республика", "").Replace("г.", "");
        return result.Trim();
    }

    /// <summary>
    /// Check for emptines and return temlate if is it true
    /// </summary>
    private string CheckNamesForEmptiness(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return _options.UnknowContactface;
        return name;
    }

    private async Task<TResponse> PostAsync<TResponse>(string url, CancellationToken token = default)
    {
        return await PostAsync<TResponse>(url, null, token);
    }

    /// <summary>
    /// Execute request and return deserialiazed data
    /// </summary>
    private async Task<TResponse> PostAsync<TResponse>(string url, string? content = null, CancellationToken token = default)
    {
        var httpClient = _httpClientFactory.CreateClient(_xOptions.HttpClientName);
        var result = await httpClient.PostAsync(url, CreateContent(content), token);
        if (result.IsSuccessStatusCode)
        {
            var responseText = await result.Content.ReadAsStringAsync(token);
            try
            {
                return JsonConvert.DeserializeObject<TResponse>(responseText);
            }
            catch (Exception ex)
            {
                throw new Exception("PostAcync result convert exception", ex);
            }
        }
        throw new Exception($"PostAcync don't handle error http status code: {result.StatusCode}, to string: {result.ToString()}, " +
            $"request url: {url}, post content: {content}");
    }

    #endregion private helper methods

    /// <summary>
    /// Get api url for checking settings 'by hands'
    /// </summary>
    public string AV100RequestString(CancellationToken token = default)
    {
        return ParamsBuilder(
            _options.AV100RequestUrl(_userSettings.AV100Token, _options.OfferListOperation, _options.OfferListCountCommand),
            _filter.YearStart, _filter.YearEnd, _filter.PriceStart, _filter.PriceEnd, _filter.DistanceStart, _filter.DistanceEnd,
            _filter.RegionExternalIds, _filter.SourceExternalIds).ToString();
    }

    /// <summary>
    /// Get car counts available for fetch by filter
    /// </summary>
    public async Task<long> AV100ReuestListCount(long fromId, long toId, CancellationToken token = default)
    {
        var url = ParamsBuilder(
            _options.AV100RequestUrl(_userSettings.AV100Token, _options.OfferListOperation, _options.OfferListCountCommand),
            _filter.YearStart, _filter.YearEnd, _filter.PriceStart, _filter.PriceEnd, _filter.DistanceStart, _filter.DistanceEnd,
            _filter.RegionExternalIds, _filter.SourceExternalIds, fromId, toId).ToString();

        var result = await PostAsync<AV100ResponseCount>(url, token);

        return result.Count;
    }

    /// <summary>
    /// Push data to XLombard API for lead creation
    /// </summary>
    public async Task<XLombardResponse> XLRequestCreateLead(string clientName, string clientPhone, string clientComment, CancellationToken token = default)
    {
        var request = Mapper.GetXLombardOrderObj(_userSettings);

        request.ClientPhone = clientPhone;
        request.ClientName = clientName;
        request.ClientComment = clientComment;

        var postData = System.Text.Json.JsonSerializer.Serialize(request);

        return await PostAsync<XLombardResponse>(
            _options.XLombardRequestUrl(_userSettings.XLombardAPIUrl, _userSettings.XLombardToken),
            postData,
            token
            );
    }

    /// <summary>
    /// Get prpfile info (amount queries, till date)
    /// </summary>
    public async Task<AV100ResponseProfile> AV100RequestProfile(CancellationToken token = default)
    {
        return await PostAsync<AV100ResponseProfile>(
            _options.AV100RequestUrl(_userSettings.AV100Token, _options.AV100ProfileOperation, string.Empty),
            token
            );
    }

    /// <summary>
    /// Get auto record by filter
    /// </summary>
    public async Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(long yearStart = default, long yearEnd = default,
            long priceStart = default, long priceEnd = default, long distanceStart = default, long distanceEnd = default,
            long[] regions = default, long[] sources = default, long fromId = default, long toId = default, CancellationToken token = default)
    {
        var items = await PostAsync<AV100ResponseOffer>(
            ParamsBuilder(_options.AV100RequestUrl(_userSettings.AV100Token, _options.OfferListOperation, _options.OfferListCommand),
            yearStart, yearEnd, priceStart, priceEnd, distanceStart, distanceEnd, regions, sources, fromId, toId).ToString(), token);

        return items.Result.ListOffer;
    }

    /// <summary>
    /// Fetch new or retro items from AV100 api by filter 
    /// </summary>
    public async Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(long fromId, long toId, CancellationToken token = default)
    {
        return await AV100GetListOffers(_filter.YearStart, _filter.YearEnd, _filter.PriceStart, _filter.PriceEnd, _filter.DistanceStart,
            _filter.DistanceEnd, _filter.RegionExternalIds, _filter.SourceExternalIds, fromId, toId, token);
    }

    /// <summary>
    /// Get full list of regions
    /// </summary>
    public async Task<ICollection<AV100Region>> AV100RequestRegions(CancellationToken token = default)
    {
        var responseRegion = await PostAsync<AV100ResponseRegion>(
            _options.AV100RequestUrl(_userSettings.AV100Token, _options.AV100DictionaryAPIOperation, _options.AV100RegionAPIParameters),
            token);

        if (responseRegion is null)
            return null;

        return responseRegion.Result.Regions.Select(x => new AV100Region
        {
            RegionId = x.RegionId,
            Name = CleanRegionName(x.RegionName)
        }).ToArray();
    }

    /// <summary>
    /// Get full list of sources
    /// </summary>
    public async Task<ICollection<AV100Source>> AV100RequestSource(CancellationToken token = default)
    {
        var responseSource = await PostAsync<AV100ResponseSource>(
            _options.AV100RequestUrl(_userSettings.AV100Token, _options.AV100DictionaryAPIOperation, _options.AV100SourceAPIParameters),
            token);

        if (responseSource is null)
            return null;

        return responseSource.Result.Sources.Select(Mapper.GetSource).ToArray();
    }

    /// <summary>
    /// Fetch retro items from AV100 api and push it to XLombard api
    /// </summary>
    public async Task<ExchangeResult> AV100LoadRetro(CancellationToken token = default)
    {
        var firstItem = await _context.AV100Records.OrderBy(r => r.AV100Id).FirstOrDefaultAsync();

        if (firstItem is null)
            throw new Exception("AV100CheckAndLoad method can't get lastId from DB");

        return await AV100CheckAndLoad(0, firstItem.AV100Id, token);
    }

    /// <summary>
    /// Overload AV100CheckAndLoad method for default request's
    /// </summary>
    public async Task<ExchangeResult> AV100CheckAndLoad(CancellationToken token = default)
    {
        var lastItem = await _context.AV100Records.OrderByDescending(r => r.AV100Id).FirstOrDefaultAsync();

        if (lastItem is null)
            throw new Exception("AV100CheckAndLoad method can't get lastId from DB");

        return await AV100CheckAndLoad(lastItem.AV100Id, 0, token);
    }

    /// <summary>
    /// Fetch new items from AV100 api and push it to XLombard api if it satisfies filter conditions
    /// </summary>
    public async Task<ExchangeResult> AV100CheckAndLoad(long fromId, long toId, CancellationToken token = default)
    {
        if (fromId == 0 && toId == 0)
            return await AV100CheckAndLoad(token);

        var availableCount = await AV100ReuestListCount(fromId, toId, token);

        if (availableCount < _filter.PackCount)
        {
            return new ExchangeResult { Result = false, Message = $"Not anought available count in av100 == {availableCount}" };
        }

        var items = await AV100GetListOffers(fromId, toId, token);

        var toInsert = new List<AV100RecordEntity>();

        foreach (var item in items)
        {
            if (string.IsNullOrWhiteSpace(item.Phone))
                continue;

            if (_context.AV100Records.Any(z => z.AV100Id == item.ID))
                continue;

            var title = $"{item.Title}, {item.Price}р., {item.Credate}, {item.Url}";
            var addToXl = await XLRequestCreateLead(
                CheckNamesForEmptiness(item.Contactface),
                item.Phone,
                title,
                token);

            toInsert.Add(new AV100RecordEntity()
            {
                Title = title,
                AV100Id = item.ID,
                SucceededUpload = long.Parse(addToXl.data) > 0,
            });
        }

        if (toInsert.Count > 0)
        {
            await _context.AV100Records.AddRangeAsync(toInsert.ToArray());
            await _context.SaveChangesAsync(token);
        }        
        return new ExchangeResult { Result = toInsert.Count > 0, SucceessfulCount = toInsert.Count, Message = "Successfuly loaded" };
    }
}