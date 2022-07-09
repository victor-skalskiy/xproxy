using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Domain;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace XProxy.Services;
public class SettingsService : ISettingsService
{
    private readonly DataContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public SettingsService(DataContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
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

        return UserSettingsMapper.GetUserSettings(userSettingsEntity);
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
        userSettingsEntity.XLombardDealTypeId = xLombardFilialId;

        _context.UserSettings.Update(userSettingsEntity);
        await _context.SaveChangesAsync(token);

        return UserSettingsMapper.GetUserSettings(userSettingsEntity);
    }

    /// <summary>
    /// Get UserSettings for index page list
    /// </summary>
    public async Task<ICollection<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default)
    {
        return await _context.UserSettings.Select(x => new UserSettingsItem
        {
            Av100Token = x.AV100Token,
            Id = x.Id,
            XLombardAPIUrl = x.XLombardAPIUrl,
            XLombardToken = x.XLombardToken,
            UpdateInterval = x.UpdateInterval

        }).ToArrayAsync();
    }

    /// <summary>
    /// Get item UserSettings for Edit page
    /// </summary>
    public async Task<UserSettings> GetSettingsItemAsync(long id, CancellationToken token = default)
    {
        return await _context.UserSettings
            .Where(x => x.Id == id)
            .Select(z => UserSettingsMapper.GetUserSettings(z)).FirstOrDefaultAsync(token);
    }

    public async Task<XLombardResponse> XLRequest(long id, CancellationToken token = default)
    {
        //var userSettingsEntity = await _context.UserSettings.Where(x => x.Id == id).FirstOrDefaultAsync();

        //if (userSettingsEntity is null)
        //    return null;

        //var userSettings = UserSettingsMapper.GetUserSettings(userSettingsEntity);

        //var request = new XLombardOrderObj()
        //{
        //    Source = userSettings.XLombardSource,
        //    ClientPhone = "+79257406105",
        //    DealTypeId = userSettings.XLombardDealTypeId,
        //    FilialId = userSettings.XLombardFilialId
        //};

        //var postData = System.Text.Json.JsonSerializer.Serialize(request);

        //var client = _httpClientFactory.CreateClient("MyBaseClient");
        //client.BaseAddress = new Uri(userSettings.XLombardAPIUrl);
        //var result = await client.PostAsync(userSettings.RequestCommand, new StringContent(postData, Encoding.UTF8, "application/json"));


        //if (result.IsSuccessStatusCode)
        //{
        //    // Read all of the response and deserialise it into an instace of
        //    // WeatherForecast class
        //    var content = await result.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<XLombardResponse>(content);
        //}
        //TODO RESOLVE IT

        return null;
    }
}