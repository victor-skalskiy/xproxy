using Microsoft.EntityFrameworkCore;
using XProxy.DAL;
using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services;

public sealed class UserSettingsStorage : IUserSettingsStorage
{
    private readonly DataContext _context;
    private readonly ISettingsService _settingsService;

    public UserSettingsStorage(DataContext context, ISettingsService settingsService)
    {
        _context = context;
        _settingsService = settingsService;
    }

    #region Implementation of IUserSettingsStorage

    public async Task<UserSettings> GetUserSettingsAsync(long userSettingsId, CancellationToken token = default)
    {
        if (_context.UserSettings.Count() == 0)
            await _settingsService.CreateTempUserSettingsAsync(token);

        var userSettings = await _context.UserSettings.FirstOrDefaultAsync(
            x => x.Id == userSettingsId,
            token);

        if (userSettings == null)
        {
            throw new Exception($"User settings was not found by id '{userSettingsId}'");
        }

        return Mapper.GetUserSettings(userSettings);
    }

    #endregion
}