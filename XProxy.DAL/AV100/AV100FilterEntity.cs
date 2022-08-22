namespace XProxy.DAL;

public class AV100FilterEntity : BaseEntityEntity
{
    public AV100FilterEntity() { }
    
    /// <summary>
    /// Car manufacture period
    /// </summary>
    public long YearStart { get; set; }
    public long YearEnd { get; set; }

    public long PriceStart { get; set; }
    public long PriceEnd { get; set; }

    public long DistanceStart { get; set; }
    public long DistanceEnd { get; set; }

    /// <summary>
    /// car need to repair
    /// </summary>
    public bool Remont { get; set; }

    /// <summary>
    /// count of new items on data set for start downloading
    /// </summary>
    public long PackCount { get; set; }

    public ICollection<AV100RegionEntity> Regions { get; set; }

    public ICollection<AV100SourceEntity> Sources { get; set; }
}