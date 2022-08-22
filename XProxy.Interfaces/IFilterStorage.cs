using XProxy.Domain;

namespace XProxy.Interfaces;

public interface IFilterStorage
{
    Task<AV100Filter> GetFilterAsync(long filterId, CancellationToken cancellationToken = default);
}