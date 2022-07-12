using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace XProxy.Domain;

public class AV100ResponseBase
{
    [JsonProperty(PropertyName = "start"), JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Start { get; set; }

    [JsonProperty(PropertyName = "end"), JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime End { get; set; }

    [JsonProperty(PropertyName = "error")]
    public bool Error { get; set; }

    [JsonProperty(PropertyName = "error_msg")]
    public string ErrorMsg { get; set; }        
}

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        base.DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
    }
}
