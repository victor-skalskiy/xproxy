﻿using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 about account status
/// </summary>
public class AV100ResponseProfile : AV100ResponseBase
{
    public AV100ResponseProfile()
    {
        Result = new AV100RsponseProfileObj { BadAccessTo = 1 };
    }

    [JsonProperty("result")]
    public AV100RsponseProfileObj Result { get; set; }
}

public class AV100RsponseProfileObj
{
    static private readonly DateTime _unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    [JsonProperty("leftClicks")]
    public long LleftClicks { get; set; }

    [JsonProperty("accessTo")]
    public long BadAccessTo { get; set; }

    [JsonIgnore]
    public DateTime AccessTo => _unixEpoch.AddSeconds(BadAccessTo);

    [JsonProperty("accessToStr")]
    public string AccessToStr { get; set; }
}
