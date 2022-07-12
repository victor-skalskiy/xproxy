namespace XProxy.Domain;

/// <summary>
/// Card element, for working with BL
/// </summary>
public class UserSettings
{
    public long Id { get; set; }

    public long UpdateInterval { get; set; }

    public string AV100Token { get; set; }

    public string XLombardAPIUrl { get; set; }

    public string XLombardToken { get; set; }

    public long XLombardFilialId { get; set; }

    public long XLombardDealTypeId { get; set; }

    public string XLombardSource { get; set; }

    public string XLombardRequestUrl => $"{XLombardAPIUrl}/handlers/requests.ashx?operation=add&token={XLombardToken}";
    public string XLombardRequestCommand => $"?operation=add&token={XLombardToken}";

    public string AV100RequestUrl(string operation, string addParameters)
        => $"https://data.av100.ru/{operation}.ashx?key={AV100Token}&{addParameters}";
}