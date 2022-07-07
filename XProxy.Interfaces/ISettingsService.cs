using XProxy.Domain;
namespace XProxy.Interfaces;
public interface ISettingsService
{
    Task<ICollection<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default);

    Task<UserSettings> CreateUserSettingsAsync(long updateInterval, string av100Token, string xLombardAPIUrl, string xLombardToken, CancellationToken token = default);
}

