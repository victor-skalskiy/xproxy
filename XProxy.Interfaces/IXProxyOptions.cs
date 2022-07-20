using System;
namespace XProxy.Interfaces
{
    public interface IXProxyOptions
    {
        // dev options, toggle internet usage
        bool UpLink { get; }
    }
}