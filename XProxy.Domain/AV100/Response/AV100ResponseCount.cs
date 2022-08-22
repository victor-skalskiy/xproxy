using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 Offer count list
/// </summary>
public class AV100ResponseCount : AV100ResponseBase
{
    public AV100ResponseCount() { }

    [JsonProperty("count")]
    public long Count { get; set; }
}