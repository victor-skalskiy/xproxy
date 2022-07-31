namespace XProxy.DAL;

public class AV100RecordEntity : BaseEntityEntity
{
    public AV100RecordEntity() { }

    public long AV100Id { get; set; }

    public string Title { get; set; }

    public bool SucceededUpload { get; set; }
}