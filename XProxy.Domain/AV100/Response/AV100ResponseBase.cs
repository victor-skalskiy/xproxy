using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XProxy.Domain;

public class AV100ResponseBase
{
    [JsonProperty("start"), JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Start { get; set; }

    [JsonProperty("end"), JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime End { get; set; }

    [JsonProperty("error")]
    public bool Error { get; set; }

    [JsonProperty("error_msg")]
    public string ErrorMsg { get; set; }        
}