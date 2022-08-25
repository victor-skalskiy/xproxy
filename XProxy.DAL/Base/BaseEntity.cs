namespace XProxy.DAL;

public class BaseEntityEntity : IEntity
{
    public long Id { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }

    public BaseEntityEntity()
    {
        IsActive = true;
        CreateDate = DateTime.UtcNow;
        ModifyDate = DateTime.UtcNow;
    }   
}