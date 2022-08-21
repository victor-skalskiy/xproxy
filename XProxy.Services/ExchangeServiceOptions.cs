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
    }

    public string AV100DictionaryAPIOperation { get; }
    
    public string AV100RegionAPIParameters { get; }
    
    public string AV100SourceAPIParameters { get; }
}