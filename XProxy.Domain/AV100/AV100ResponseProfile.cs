using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 about account status
/// </summary>
public class AV100ResponseProfile : AV100ResponseBase
{
    [JsonProperty(PropertyName = "result")]
    public AV100RsponseProfileObj Result { get; set; }
}

public class AV100RsponseProfileObj
{
    static private readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [JsonProperty(PropertyName = "leftClicks")]
    public long LleftClicks { get; set; }

    [JsonProperty(PropertyName = "accessTo")]
    public long BadAccessTo { get; set; }

    [JsonIgnore]
    public DateTime AccessTo => _unixEpoch.AddSeconds(BadAccessTo);

    [JsonProperty(PropertyName = "accessToStr")]
    public string AccessToStr { get; set; }
}    
