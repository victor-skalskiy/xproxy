namespace XProxy.DAL;

/// <summary>
/// https://data.av100.ru/dictionaries.ashx?key=APIKEY&methodname=offerssource
/// </summary>
public class AV100SourceEntity : BaseEntityEntity
{
    public AV100SourceEntity() { }

    public long AV100SourceId { get; set; }
    public string Name { get; set; }

    public ICollection<AV100FilterEntity> Filters { get; set; }
    
}