using XProxy.Interfaces;
using XProxy.Domain;
using Newtonsoft.Json;
using System.Text;

namespace XProxy.Services;

public sealed class ExchangeService : IExchangeService
{
    private const string HttpClientName = "MyBaseClient";
    private const string JsonContentType = "application/json";
    private readonly UserSettings _userSettings;
    private readonly ExchangeServiceOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;

    public ExchangeService(
        UserSettings userSettings,
        ExchangeServiceOptions options,
        IHttpClientFactory httpClientFactory)
    {
        _userSettings = userSettings;
        _options = options;
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Connect to XLombard
    /// </summary>
    public async Task<XLombardResponse> XLRequest(long id, CancellationToken token = default)
    {
        var request = Mapper.GetXLombardOrderObj(_userSettings);

        request.ClientPhone = "+79257406105";
        request.ClientName = "Василий Кузьмич";

        var postData = System.Text.Json.JsonSerializer.Serialize(request);

        var client = _httpClientFactory.CreateClient(HttpClientName);

        var result = await client.PostAsync(
            _userSettings.XLombardRequestUrl, 
            CreateContent(postData),
            token);
        
        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync(token);
            return JsonConvert.DeserializeObject<XLombardResponse>(content);
        }

        //TODO: log fail
        return new XLombardResponse();
    }

    public async Task<AV100ResponseProfile> AV100RequestProfile(long userSettingsId, CancellationToken token = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var result = await client.PostAsync(
            _userSettings.AV100RequestUrl("profile", string.Empty),
            CreateContent(string.Empty),
            token);

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync(token);
            try
            {
                return JsonConvert.DeserializeObject<AV100ResponseProfile>(content);
            }
            catch (Exception ex)
            {
                //TODO: log fail
                return new AV100ResponseProfile();
            }
        }

        //TODO: log fail
        return null;
    }


    public Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(string key, string command, long regionId,
        long priceStart, long remont = 0, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    private static string CleanRegionName(string durtyName)
    {
        var result = durtyName;
        if (result.Contains('('))
            result = result.Substring(0, durtyName.IndexOf('('));

        result = result.Replace("Республика", "").Replace("г.", "");
        return result.Trim();
    }

    /// <summary>
    /// Get full list of regions
    /// </summary>
    public async Task<ICollection<AV100Region>> AV100RequestRegions(CancellationToken token = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var result = await client.PostAsync(
            _userSettings.AV100RequestUrl(_options.AV100DictionaryAPIOperation, _options.AV100RegionAPIParameters),
            CreateContent(string.Empty),
            token);

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync(token);
            var response = JsonConvert.DeserializeObject<AV100ResponseRegion>(content) ?? new AV100ResponseRegion();
            var regionList = response.Result.Regions.Select(x => new AV100Region
            {
                RegionId = x.RegionId,
                Name = CleanRegionName(x.RegionName)
            });
            return regionList.ToArray();
        }

        //TODO: log fail
        return null;
    }

    /// <summary>
    /// Get full list of sources
    /// </summary>
    public async Task<ICollection<AV100Source>> AV100RequestSource(CancellationToken token = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        var result = await client.PostAsync(
            _userSettings.AV100RequestUrl(_options.AV100DictionaryAPIOperation, _options.AV100SourceAPIParameters),
            CreateContent(string.Empty),
            token);

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync(token);
            var response = JsonConvert.DeserializeObject<AV100ResponseSource>(content) ?? new AV100ResponseSource();
            return response.Result.Sources.Select(Mapper.GetSource).ToArray();
        }

        //TODO: log fail
        return null;
    }

    private static StringContent CreateContent(string content)
    {
        return new StringContent(content, Encoding.UTF8, JsonContentType);
    }
}