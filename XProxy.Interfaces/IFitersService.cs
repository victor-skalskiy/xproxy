using XProxy.Domain;
namespace XProxy.Interfaces;

public interface IFiltersService
{
    // Getting list of filters, for index page
    Task<ICollection<AV100FilterItem>> GetFiltersAsync(CancellationToken token = default);

    // Getting specific filter item for edit page or api requests
    Task<AV100Filter> GetFilterItemAsync(long id, CancellationToken token = default);

    // Creating filter
    Task<AV100Filter> CreateFilterAsync(long YearStart, string YearEnd, string PriceStart, string PriceEnd, long DistanceStart,
        long DistanceEnd, long CarCount, long PhoneCount, long Regionid, CancellationToken token = default);

    // Updating filter
    Task<AV100Filter> UpdateFilterAsync(long id, long YearStart, string YearEnd, string PriceStart, string PriceEnd, long DistanceStart, long DistanceEnd,
        long CarCount, long PhoneCount, long Regionid, CancellationToken token = default);
}