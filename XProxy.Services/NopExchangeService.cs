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

    public Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(long yearStart = default, long yearEnd = default,
            long priceStart = default, long priceEnd = default, long distanceStart = default, long distanceEnd = default,
            long[] regions = default, long[] sources = default, long fromId = default, long toId = default,
            CancellationToken token = default)
    {
        return Task.FromResult<ICollection<AV100ResponseOfferResultRow>>(Array.Empty<AV100ResponseOfferResultRow>());
    }

    public Task<XLombardResponse> XLRequestCreateLead(string clientName, string clientPhone, string clientComment, CancellationToken token = default)
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

    public Task<long> AV100ReuestListCount(long fromId, long toId, CancellationToken token = default)
    {
        return Task.FromResult(new long());
    }

    public string AV100RequestString(CancellationToken token = default)
    {
        return string.Empty;
    }

    public Task<ExchangeResult> AV100CheckAndLoad(long fromId, long toId, CancellationToken token = default)
    {
        return Task.FromResult(new ExchangeResult());
    }

    public Task<ICollection<AV100ResponseOfferResultRow>> AV100GetListOffers(long fromId, long toId, CancellationToken token = default)
    {
        return Task.FromResult<ICollection<AV100ResponseOfferResultRow>>(Array.Empty<AV100ResponseOfferResultRow>());
    }

    public Task<ExchangeResult> AV100CheckAndLoad(CancellationToken token = default)
    {
        return Task.FromResult(new ExchangeResult());
    }

    public Task<ExchangeResult> AV100LoadRetro(CancellationToken token = default)
    {
        return Task.FromResult(new ExchangeResult());
    }

    #endregion
}