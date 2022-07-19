using Microsoft.Extensions.Configuration;
using XProxy.Interfaces;

namespace XProxy.Services;

public class XProxyOptions : IXProxyOptions
{
    private readonly bool _uplink;
    public XProxyOptions(IConfiguration configuration)
    {
        _uplink = configuration.GetSection("AppSetting").GetSection("Uplink").Value == "True";
    }

    bool IXProxyOptions.UpLink => _uplink;
}
