using XProxy.Domain;
namespace XProxy.Interfaces;

public interface IFiltersService
{
    /// <summary>
    /// Getting list of filters, for index page
    /// </summary>
    Task<List<AV100FilterItem>> GetFiltersAsync(CancellationToken token = default);

    /// <summary>
    /// Getting specific filter item for edit page or api requests
    /// </summary>
    Task<AV100Filter> GetFilterAsync(long id, CancellationToken token = default);   

    /// <summary>
    /// Creating filter
    /// </summary>
    Task<AV100Filter> CreateFilterAsync(long yearStart, long yearEnd, long priceStart, long priceEnd, long distanceStart,
        long distanceEnd, long packCount, bool remont, List<long> regionIds, List<long> sourceIds, CancellationToken token = default);

    /// <summary>
    /// Creating temporary filter (1 launch)
    /// </summary>
    Task<AV100Filter> CreateTempFilterAsync(CancellationToken token = default);

    /// <summary>
    /// Updating filter
    /// </summary>
    Task<AV100Filter> UpdateFilterAsync(long id, long yearStart, long yearEnd, long priceStart, long priceEnd, long distanceStart,
        long distanceEnd, long packCount, bool remont, List<long> regionIds, List<long> sourceIds, CancellationToken token = default);

    /// <summary>
    /// Get list of source items for controls
    /// </summary>
    Task<ICollection<AV100Source>> GetSourcesAsync(CancellationToken token = default);

    /// <summary>
    /// Create Source 
    /// </summary>
    Task<AV100Source> CreateSourceAsync(long id, string name, CancellationToken token = default);

    /// <summary>
    /// Create lot of Sources
    /// </summary>
    Task<ICollection<AV100Source>> CreateSourcesAsync(Dictionary<long, string> data, CancellationToken token = default);

    /// <summary>
    /// Get list of Region for controls
    /// </summary>
    Task<ICollection<AV100Region>> GetRegionsAsync(CancellationToken token = default);

    /// <summary>
    /// Create Region 
    /// </summary>
    Task<AV100Region> CreateRegionAsync(long id, string name, CancellationToken token = default);

    /// <summary>
    /// Create many Regions
    /// </summary>
    Task<ICollection<AV100Region>> CreateRegionsAsync(Dictionary<long, string> data, CancellationToken token = default);
}