using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 about Source dictionary
/// </summary>
public class AV100ResponseSource : AV100ResponseBase
{
    public AV100ResponseSource() { Result = new AV100ResponseSourceResult { }; }

    [JsonProperty("result")]
    public AV100ResponseSourceResult Result { get; set; }
}

public class AV100ResponseSourceResult
{
    public AV100ResponseSourceResult() { Sources = new List<AV100ResponseSourceResultRow>(); }

    [JsonProperty("Sources")]
    public List<AV100ResponseSourceResultRow> Sources { get; set; }
}

public class AV100ResponseSourceResultRow
{
    [JsonProperty("SourceId")]
    public long SourceId { get; set; }
    [JsonProperty("SourceName")]
    public string SourceName { get; set; }
}