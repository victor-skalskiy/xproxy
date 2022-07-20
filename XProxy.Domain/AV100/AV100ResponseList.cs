using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 Offer List
/// </summary>
public class AV100ResponseList : AV100ResponseBase
{
    public AV100ResponseList() { }

    [JsonProperty(PropertyName = "result")]
    public AV100ResponseResult Result { get; set; }
}