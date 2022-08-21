using System;
namespace XProxy.Interfaces
{
    public interface IXProxyOptions
    {
        /// <summary>
        /// dev options, toggle internet usage
        /// </summary>
        bool UpLink { get; }
         
        /// <summary>
        /// Link to UserSettingsEntity for getting API's urls
        /// </summary>
        long DefaultUserSettingsId { get; }

        string AV100DictionaryAPIOperation { get; }

        string AV100RegionAPIParameters { get; }

        string AV100SourceAPIParameters { get; }
    }
}