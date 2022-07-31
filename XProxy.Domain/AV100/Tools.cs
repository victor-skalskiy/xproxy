using Newtonsoft.Json.Converters;

namespace XProxy.Domain;

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        base.DateTimeFormat = "dd.MM.yyyy HH:mm:ss";
    }
}