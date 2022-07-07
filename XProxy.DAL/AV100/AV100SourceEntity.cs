using System;
namespace XProxy.DAL
{
    /// <summary>
    /// https://data.av100.ru/dictionaries.ashx?key=APIKEY&methodname=offerssource
    /// </summary>
    public class AV100SourceEntity : BaseEntityEntity
    {
        public AV100SourceEntity() { }

        public long SourceId { get; set; }
        public string SourceName { get; set; }
    }
}