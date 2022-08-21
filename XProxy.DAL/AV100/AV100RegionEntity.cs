using System.ComponentModel.DataAnnotations.Schema;

namespace XProxy.DAL;

/// <summary>
/// https://data.av100.ru/dictionaries.ashx?key=APIKEY&name=offersregion
/// </summary>
public class AV100RegionEntity : BaseEntityEntity
{
    public AV100RegionEntity() { }

    public long AV100RegionId { get; set; }
    public string Name { get; set; }   

    public ICollection<AV100FilterEntity> Filters { get; set; }
}