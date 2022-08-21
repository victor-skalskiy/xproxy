using XProxy.Domain;

namespace XProxy.Interfaces;

public interface IExchangeService
{
    /// <summary>
    /// Get data about account (paid till, request amount etc)
    /// </summary>
    Task<AV100ResponseProfile> AV100RequestProfile(CancellationToken token = default);

    /// <summary>
    /// Get data from AV100 filters match items
    /// </summary>
    Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(string key, string command, long regionId, long priceStart,
        long remont = 0, CancellationToken token = default);

    Task<XLombardResponse> XLRequest(long id, CancellationToken token = default);

    /// <summary>
    /// Get full list of regions from API
    /// </summary>
    Task<ICollection<AV100Region>> AV100RequestRegions(CancellationToken token = default);


    /// <summary>
    /// Get full list of source from API
    /// </summary>
    Task<ICollection<AV100Source>> AV100RequestSource(CancellationToken token = default);
}