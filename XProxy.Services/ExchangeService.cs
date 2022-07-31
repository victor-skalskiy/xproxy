using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace XProxy.Services;

public class ExchangeService : IExchangeService
{
    private readonly DataContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IXProxyOptions _xProxyOptions;
    private readonly UserSettings _userSettings;


    public ExchangeService(DataContext context, IHttpClientFactory httpClientFactory, IXProxyOptions xProxyOptions)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _xProxyOptions = xProxyOptions;
        _userSettings = InitUserSettings();
    }

    private UserSettings InitUserSettings()
    {
        if (_xProxyOptions.UpLink)
        {
            var userSettingsEntity = _context.UserSettings.Where(x => x.Id == _xProxyOptions.DefaultUserSettingsId).FirstOrDefault();
            if (userSettingsEntity != null)
                return Mapper.GetUserSettings(userSettingsEntity);
        }

        return null;
    }

    /// <summary>
    /// Connect to XLombard
    /// </summary>
    public async Task<XLombardResponse> XLRequest(long id, CancellationToken token = default)
    {
        if (!_xProxyOptions.UpLink)
            return new XLombardResponse();


        var request = Mapper.GetXLombardOrderObj(_userSettings);

        request.ClientPhone = "+79257406105";
        request.ClientName = "Василий Кузьмич";

        var postData = System.Text.Json.JsonSerializer.Serialize(request);

        var client = _httpClientFactory.CreateClient("MyBaseClient");

        var result = await client.PostAsync(_userSettings.XLombardRequestUrl, new StringContent(postData, Encoding.UTF8, "application/json"));


        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<XLombardResponse>(content);
        }

        //TODO: log fail
        return new XLombardResponse();
    }

    public async Task<AV100ResponseProfile> AV100RequestProfile(long userSettingsId, CancellationToken token = default)
    {
        if (!_xProxyOptions.UpLink)
            return new AV100ResponseProfile();

        var client = _httpClientFactory.CreateClient("MyBaseClient");
        var result = await client.PostAsync(_userSettings.AV100RequestUrl("profile", string.Empty), new StringContent(string.Empty, Encoding.UTF8, "application/json"));

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
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
        if (!_xProxyOptions.UpLink)
            return null;

        var client = _httpClientFactory.CreateClient("MyBaseClient");
        var result = await client.PostAsync(
            _userSettings.AV100RequestUrl(_xProxyOptions.AV100DictionaryAPIOperation, _xProxyOptions.AV100RegionAPIParameters),
            new StringContent(string.Empty, Encoding.UTF8, "application/json"));

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
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
        if (!_xProxyOptions.UpLink)
            return null;

        var client = _httpClientFactory.CreateClient("MyBaseClient");
        var result = await client.PostAsync(
            _userSettings.AV100RequestUrl(_xProxyOptions.AV100DictionaryAPIOperation, _xProxyOptions.AV100SourceAPIParameters),
            new StringContent(string.Empty, Encoding.UTF8, "application/json"));

        if (result.IsSuccessStatusCode)
        {
            var content = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<AV100ResponseSource>(content) ?? new AV100ResponseSource();
            return response.Result.Sources.Select(x => Mapper.GetSource(x)).ToArray();
        }

        //TODO: log fail
        return null;
    }
}