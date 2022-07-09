using XProxy.Domain;
namespace XProxy.Interfaces;

public interface ISettingsService
{
    Task<ICollection<UserSettingsItem>> GetSettingsAsync(CancellationToken token = default);

    Task<UserSettings> GetSettingsItemAsync(long id, CancellationToken token = default);

    Task<UserSettings> CreateUserSettingsAsync(long updateInterval, string av100Token, string xLombardAPIUrl, string xLombardToken, long xLombardFilialId,
        long xLombardDealTypeId, string xLombardSource, CancellationToken token = default);

    Task<UserSettings> UpdateUserSettingsAsync(long id, long updateInterval, string av100Token, string xLombardAPIUrl, string xLombardToken, long xLombardFilialId,
        long xLombardDealTypeId, string xLombardSource, CancellationToken token = default);

    Task<XLombardResponse> XLRequest(long id, CancellationToken token = default);
}

