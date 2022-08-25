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
    Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(long yearStart = default, long yearEnd = default,
            long priceStart = default, long priceEnd = default, long distanceStart = default, long distanceEnd = default,
            long[] regions = default, long[] sources = default, long fromId = default, long toId = default,
            CancellationToken token = default);

    Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(long fromId, long toId, CancellationToken token = default);

    Task<XLombardResponse> XLRequest(long id, CancellationToken token = default);

    Task<XLombardResponse> XLRequestCreateLead(string clientName, string clientPhone, string clientComment,
        CancellationToken token = default);

    /// <summary>
    /// Get full list of regions from API
    /// </summary>
    Task<ICollection<AV100Region>> AV100RequestRegions(CancellationToken token = default);


    /// <summary>
    /// Get full list of source from API
    /// </summary>
    Task<ICollection<AV100Source>> AV100RequestSource(CancellationToken token = default);

    /// Get counts of expected result values
    /// </summary>
    Task<long> AV100ReuestListCount(long fromId, long toId, CancellationToken token = default);


    Task<string> AV100RequestString(CancellationToken token = default);


    Task<ExchangeResult> AV100CheckAndLoad(long fromId, long toId, CancellationToken token = default);

    Task<ExchangeResult> AV100CheckAndLoad(CancellationToken token = default);

    Task<ExchangeResult> AV100LoadRetro(CancellationToken token = default);
}