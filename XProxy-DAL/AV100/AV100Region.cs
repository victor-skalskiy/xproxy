using System;
namespace XProxy.DAL
{
    /// <summary>
    /// https://data.av100.ru/dictionaries.ashx?key=APIKEY&methodname=offersregion
    /// </summary>
    public class AV100Region
    {
        public AV100Region() { }
        public long RegionId { get; set; }
        public string RegionName { get; set; }
    }
}

