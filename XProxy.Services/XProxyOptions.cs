using Microsoft.Extensions.Configuration;
using XProxy.Interfaces;

namespace XProxy.Services;

public class XProxyOptions : IXProxyOptions
{
    public XProxyOptions(IConfiguration configuration)
    {
        UpLink = configuration.GetSection("AppSetting").GetSection("Uplink").Value == "True";
        DefaultUserSettingsId = 1;
        AV100DictionaryAPIOperation = "dictionaries";
        AV100RegionAPIParameters = "name=offersregion";
        AV100SourceAPIParameters = "name=offerssource";
        DefaultFilterId = 1;
    }

    /// <summary>
    /// Exist internet connection, option for local development and debug
    /// </summary>
    public bool UpLink { get; }

    /// <summary>
    /// Hardcode UserSettingsEntityId for getting API's urls
    /// </summary>
    public long DefaultUserSettingsId { get; }

    public long DefaultFilterId { get; }

    public string AV100DictionaryAPIOperation { get; }

    public string AV100RegionAPIParameters { get; }

    public string AV100SourceAPIParameters { get; }
}
