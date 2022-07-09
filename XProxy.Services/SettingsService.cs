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

    public async Task<UserSettings> CreateUserSettingsAsync(long updateInterval, string av100Token, string xLombardAPIUrl,
        string xLombardToken, CancellationToken token = default)
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
        };

        _context.UserSettings.Add(userSettingsEntity);
        await _context.SaveChangesAsync(token);

        return new UserSettings()
        {
            Av100Token = userSettingsEntity.AV100Token,
            Id = userSettingsEntity.Id,
            XLombardAPIUrl = userSettingsEntity.XLombardAPIUrl,
            XLombardToken = userSettingsEntity.XLombardToken,
            UpdateInterval = userSettingsEntity.UpdateInterval
        };
    }

    public async Task<ICollection<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default)
    {
        return await _context.UserSettings.Select(x => new UserSettingsItem
        {
            UpdateInterval = x.UpdateInterval,
            YearEnd = x.AV100Filter.YearEnd,
            YearStart = x.AV100Filter.YearStart
        }).ToArrayAsync();
    }
}