using XProxy.Domain;
namespace XProxy.Interfaces;

public interface ISettingsService
{
    Task<List<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default);

    Task<UserSettings> GetSettingsItemAsync(long id, CancellationToken token = default);

    Task<UserSettings> CreateUserSettingsAsync(string av100Token, string xLombardAPIUrl, string xLombardToken, long xLombardFilialId,
        long xLombardDealTypeId, string xLombardSource, CancellationToken token = default);

    Task<UserSettings> CreateTempUserSettingsAsync(CancellationToken token = default);

    Task<UserSettings> UpdateUserSettingsAsync(long id, string av100Token, string xLombardAPIUrl, string xLombardToken, long xLombardFilialId,
        long xLombardDealTypeId, string xLombardSource, CancellationToken token = default);    
}