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


    Task<Av100Filter> GetFilterItemAsync(long id, CancellationToken token = default);

    Task<ICollection<Av100FilterItem>> GetFiltersAsync(CancellationToken token = default);

    Task<Av100Filter> CreateFilterAsync(long YearStart, string YearEnd, string PriceStart, string PriceEnd, long DistanceStart,
        long DistanceEnd, long CarCount, long PhoneCount, long Regionid, CancellationToken token = default);

    Task<Av100Filter> UpdateFilterAsync(long id, long YearStart, string YearEnd, string PriceStart, string PriceEnd, long DistanceStart, long DistanceEnd,
        long CarCount, long PhoneCount, long Regionid, CancellationToken token = default);

}

