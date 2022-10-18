using System;
namespace XProxy.Interfaces
{
    public interface IXProxyOptions
    {
        /// <summary>
        /// Link to UserSettingsEntity for getting API's urls
        /// </summary>
        long DefaultUserSettingsId { get; }

        long DefaultFilterId { get; }

        string AV100DictionaryAPIOperation { get; }

        string AV100RegionAPIParameters { get; }

        string AV100SourceAPIParameters { get; }

        string HttpClientName { get; }
    }
}