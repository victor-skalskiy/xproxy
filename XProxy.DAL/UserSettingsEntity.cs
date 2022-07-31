namespace XProxy.DAL;

public class UserSettingsEntity : BaseEntityEntity
{
    public UserSettingsEntity() { }
   
    public string XLombardAPIUrl { get; set; }

    public string XLombardToken { get; set; }

    public long XLombardFilialId { get; set; }

    public long XLombardDealTypeId { get; set; }

    public string XLombardSource { get; set; }

    public string AV100Token { get; set; }

    /// <summary>
    /// Interval for fetch data from AV100 and push to XLombard
    /// </summary>
    public long UpdateInterval { get; set; }

    /// <summary>
    /// Date of last syncing
    /// </summary>
    public DateTime LastSyncDate { get; set; }
}