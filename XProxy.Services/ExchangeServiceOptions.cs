namespace XProxy.Services;

public sealed class ExchangeServiceOptions
{
    public ExchangeServiceOptions(
        string av100DictionaryApiOperation,
        string av100RegionApiParameters,
        string av100SourceApiParameters)
    {
        AV100DictionaryAPIOperation = av100DictionaryApiOperation;
        AV100RegionAPIParameters = av100RegionApiParameters;
        AV100SourceAPIParameters = av100SourceApiParameters;
        OfferListCountCommand = "command=countlist";
        OfferListOperation = "offer";

    }

    public string AV100DictionaryAPIOperation { get; }
    
    public string AV100RegionAPIParameters { get; }
    
    public string AV100SourceAPIParameters { get; }

    public string AV100RequestUrl(string aV100Token,  string operation, string addParameters)
        => $"https://data.av100.ru/{operation}.ashx?key={aV100Token}&{addParameters}";

    public string XLombardRequestUrl(string xLombardAPIUrl, string xLombardToken)
        => $"{xLombardAPIUrl}/handlers/requests.ashx?operation=add&token={xLombardToken}";

    public string OfferListCountCommand { get; }

    public string OfferListOperation { get; }
}