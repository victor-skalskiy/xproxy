using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 about Region dictionary
/// </summary>
public class AV100ResponseRegion : AV100ResponseBase
{
    public AV100ResponseRegion() { Result = new AV100ResponseRegionResult(); }

    [JsonProperty("result")]
    public AV100ResponseRegionResult Result { get; set; }
}

public class AV100ResponseRegionResult
{
    public AV100ResponseRegionResult() { Regions = new List<AV100ResponseRegionResultRow>(); }

    [JsonProperty("Regions")]
    public List<AV100ResponseRegionResultRow> Regions { get; set; }
}

public class AV100ResponseRegionResultRow
{
    [JsonProperty("RegionId")]
    public long RegionId { get; set; }

    [JsonProperty("RegionName")]
    public string RegionName { get; set; }
}