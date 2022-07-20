using Microsoft.Extensions.Configuration;
using XProxy.Interfaces;

namespace XProxy.Services;

public class XProxyOptions : IXProxyOptions
{
    public XProxyOptions(IConfiguration configuration)
    {
        UpLink = configuration.GetSection("AppSetting").GetSection("Uplink").Value == "True";
    }

    // exist internet connection, option for local development and debug
    public bool UpLink { get; }
}
