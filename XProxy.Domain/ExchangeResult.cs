
namespace XProxy.Domain;

public class ExchangeResult
{
    public ExchangeResult()
    {
    }

    public bool Result { get; set; }
    public long SucceessfulCount { get; set; }
    public long LastId { get; set; }
    public long FirstId { get; set; }
    public string? Message { get; set; } 
}