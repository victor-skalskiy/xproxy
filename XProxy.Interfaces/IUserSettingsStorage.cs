using XProxy.Domain;

namespace XProxy.Interfaces;

public interface IUserSettingsStorage
{
    Task<UserSettings> GetUserSettingsAsync(long userSettingsId, CancellationToken cancellationToken = default);
}