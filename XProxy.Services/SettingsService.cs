using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Domain;
using Microsoft.EntityFrameworkCore;

namespace XProxy.Services;

public class SettingsService : ISettingsService
{
    private readonly DataContext _context;

    public SettingsService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Insert new UserSettings from page Create
    /// </summary>
    public async Task<UserSettings> CreateUserSettingsAsync(long updateInterval, string av100Token, string xLombardAPIUrl,
        string xLombardToken, long xLombardFilialId, long xLombardDealTypeId, string xLombardSource, CancellationToken token = default)
    {
        var userSettingsEntity = Mapper.FillSettingsEntity(new UserSettingsEntity(), updateInterval, av100Token, xLombardAPIUrl,
            xLombardToken, xLombardFilialId, xLombardDealTypeId, xLombardSource);

        _context.UserSettings.Add(userSettingsEntity);
        await _context.SaveChangesAsync(token);

        return Mapper.GetUserSettings(userSettingsEntity);
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

        userSettingsEntity = Mapper.FillSettingsEntity(userSettingsEntity, updateInterval, av100Token, xLombardAPIUrl, xLombardToken,
            xLombardFilialId, xLombardDealTypeId, xLombardSource);

        _context.UserSettings.Update(userSettingsEntity);
        await _context.SaveChangesAsync(token);

        return Mapper.GetUserSettings(userSettingsEntity);
    }

    /// <summary>
    /// Get UserSettings for index page list
    /// </summary>
    public async Task<List<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default)
    {
        return await _context.UserSettings.Select(x => Mapper.GetUserSettingsItem(x)).ToListAsync();
    }

    /// <summary>
    /// Get item UserSettings for Edit page
    /// </summary>
    public async Task<UserSettings> GetSettingsItemAsync(long id, CancellationToken token = default)
    {
        return await _context.UserSettings
            .Where(x => x.Id == id)
            .Select(z => Mapper.GetUserSettings(z)).FirstOrDefaultAsync(token);
    }
}