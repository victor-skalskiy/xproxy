using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Domain;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace XProxy.Services;

public class SettingsService : ISettingsService
{
    private readonly DataContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IXProxyOptions _xProxyOptions;


    public SettingsService(DataContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration,
        IXProxyOptions xProxyOptions)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _xProxyOptions = xProxyOptions;
    }

    /// <summary>
    /// Insert new UserSettings from page Create
    /// </summary>
    public async Task<UserSettings> CreateUserSettingsAsync(long updateInterval, string av100Token, string xLombardAPIUrl,
        string xLombardToken, long xLombardFilialId, long xLombardDealTypeId, string xLombardSource, CancellationToken token = default)
    {
        var userSettingsEntity = new UserSettingsEntity()
        {
            AV100Token = av100Token,
            IsActive = true,
            UpdateInterval = updateInterval,
            XLombardAPIUrl = xLombardAPIUrl,
            XLombardToken = xLombardToken,
            CreateDate = DateTime.UtcNow,
            LastSyncDate = DateTime.UtcNow,
            ModifyDate = DateTime.UtcNow,
            XLombardFilialId = xLombardFilialId,
            XLombardSource = xLombardSource,
            XLombardDealTypeId = xLombardFilialId
        };

        _context.UserSettings.Add(userSettingsEntity);
        await _context.SaveChangesAsync(token);

        return SettingsMapper.GetUserSettings(userSettingsEntity);
    }

    /// <summary>
    /// Update UserSettings in db from Edit page
    /// </summary>
    public async Task<UserSettings> UpdateUserSettingsAsync(long id, long updateInterval, string av100Token, string xLombardAPIUrl, string xLombardToken,
        long xLombardFilialId, long xLombardDealTypeId, string xLombardSource, CancellationToken token = default)
    {
        var userSettingsEntity = await _context.UserSettings.Where(x => x.Id == id).FirstOrDefaultAsync(token);

        if (userSettingsEntity is null)
            throw new Exception($"Can't get UserSettingsEntity by id = {id}");

        userSettingsEntity.AV100Token = av100Token;
        userSettingsEntity.UpdateInterval = updateInterval;
        userSettingsEntity.XLombardAPIUrl = xLombardAPIUrl;
        userSettingsEntity.XLombardToken = xLombardToken;
        userSettingsEntity.ModifyDate = DateTime.UtcNow;
        userSettingsEntity.XLombardFilialId = xLombardFilialId;
        userSettingsEntity.XLombardSource = xLombardSource;
        userSettingsEntity.XLombardDealTypeId = xLombardDealTypeId;

        _context.UserSettings.Update(userSettingsEntity);
        await _context.SaveChangesAsync(token);

        return SettingsMapper.GetUserSettings(userSettingsEntity);
    }

    /// <summary>
    /// Get UserSettings for index page list
    /// </summary>
    public async Task<ICollection<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default)
    {
        return await _context.UserSettings.Select(x => SettingsMapper.GetUserSettingsItem(x)).ToArrayAsync();
    }

    /// <summary>
    /// Get item UserSettings for Edit page
    /// </summary>
    public async Task<UserSettings> GetSettingsItemAsync(long id, CancellationToken token = default)
    {
        return await _context.UserSettings
            .Where(x => x.Id == id)
            .Select(z => SettingsMapper.GetUserSettings(z)).FirstOrDefaultAsync(token);
    }

    /// <summary>
    /// Connect to XLombard
    /// </summary>
    public async Task<XLombardResponse> XLRequest(long id, CancellationToken token = default)
    {
        if (!_xProxyOptions.UpLink)
            return new XLombardResponse();

        var userSettingsEntity = await _context.UserSettings.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (userSettingsEntity is null)
            return null;

        var userSettings = SettingsMapper.GetUserSettings(userSettingsEntity);

        var request = SettingsMapper.GetXLombardOrderObj(userSettings);

        request.ClientPhone = "+79257406105";
        request.ClientName = "Василий Кузьмич";

        var postData = System.Text.Json.JsonSerializer.Serialize(request);

        var client = _httpClientFactory.CreateClient("MyBaseClient");

        var result = await client.PostAsync(userSettings.XLombardRequestUrl, new StringContent(postData, Encoding.UTF8, "application/json"));


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

        var userSettingsEntity = await _context.UserSettings.Where(x => x.Id == userSettingsId).FirstOrDefaultAsync();
        var userSettings = SettingsMapper.GetUserSettings(userSettingsEntity);

        var client = _httpClientFactory.CreateClient("MyBaseClient");
        var result = await client.PostAsync(userSettings.AV100RequestUrl("profile", string.Empty), new StringContent(string.Empty, Encoding.UTF8, "application/json"));

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
    

    // Getting list of filters, for index page
    public async Task<ICollection<AV100FilterItem>> GetFiltersAsync(CancellationToken token = default)
    {
        return await _context.AV100Filters
            //.Where(a => a.UserSettingsEntityId == UserSettingsId)
            .Select(x => SettingsMapper.GetFilterItem(x)).ToArrayAsync();
    }

    // Getting specific filter item for edit page or api requests
    public async Task<AV100Filter> GetFilterItemAsync(long id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    // Creating filter
    public async Task<AV100Filter> CreateFilterAsync(long YearStart, string YearEnd, string PriceStart, string PriceEnd,
        long DistanceStart, long DistanceEnd, long CarCount, long PhoneCount, long Regionid, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    // Updating filter
    public async Task<AV100Filter> UpdateFilterAsync(long id, long YearStart, string YearEnd, string PriceStart, string PriceEnd,
        long DistanceStart, long DistanceEnd, long CarCount, long PhoneCount, long Regionid, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }




    public Task<ICollection<AV100ResponseResultListOffer>> AV100GetListOffers(string key, string command, long regionId,
        long priceStart, long remont = 0, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}