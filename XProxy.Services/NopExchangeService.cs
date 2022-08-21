using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services;

public sealed class NopExchangeService : IExchangeService
{
    #region Implementation of IExchangeService

    public Task<AV100ResponseProfile> AV100RequestProfile(long userSettingsId, CancellationToken token = default)
    {
        return Task.FromResult(new AV100ResponseProfile());
    }

    public Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(string key, string command, long regionId, long priceStart, long remont = 0,
        CancellationToken token = default)
    {
        return Task.FromResult<ICollection<AV100ResponseOfferResultRow>>(Array.Empty<AV100ResponseOfferResultRow>());
    }

    public Task<XLombardResponse> XLRequest(long id, CancellationToken token = default)
    {
        return Task.FromResult(new XLombardResponse());
    }

    public Task<ICollection<AV100Region>> AV100RequestRegions(CancellationToken token = default)
    {
        return Task.FromResult<ICollection<AV100Region>>(Array.Empty<AV100Region>());
    }

    public Task<ICollection<AV100Source>> AV100RequestSource(CancellationToken token = default)
    {
        return Task.FromResult<ICollection<AV100Source>>(Array.Empty<AV100Source>());
    }

    public Task<AV100ResponseProfile> AV100RequestProfile(CancellationToken token = default)
    {
        return Task.FromResult(new AV100ResponseProfile());
    }

    #endregion
}