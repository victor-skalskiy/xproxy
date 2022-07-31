using Microsoft.EntityFrameworkCore;
using XProxy.DAL;
using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services;

public sealed class UserSettingsStorage : IUserSettingsStorage
{
    private readonly DataContext _context;
    
    public UserSettingsStorage(DataContext context)
    {
        _context = context;
    }
    
    #region Implementation of IUserSettingsStorage

    public async Task<UserSettings> GetUserSettingsAsync(long userSettingsId, CancellationToken cancellationToken = default)
    {
        var userSettings = await _context.UserSettings.FirstOrDefaultAsync(
            x => x.Id == userSettingsId, 
            cancellationToken);

        if (userSettings == null)
        {
            throw new Exception($"User settings was not found by id '{userSettingsId}'");
        }

        return Mapper.GetUserSettings(userSettings);
    }

    #endregion
}