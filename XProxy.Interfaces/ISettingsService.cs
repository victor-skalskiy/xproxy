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



    Task<AV100ResponseProfile> AV100RequestProfile(long userSettingsId, CancellationToken token = default);

    Task<ICollection<AV100ResponseResultListOffer>> AV100GetListOffers(string key, string command, long regionId, long priceStart,
        long remont = 0, CancellationToken token = default);
}