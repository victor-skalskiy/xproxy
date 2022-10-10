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
    /// Date of last syncing
    /// </summary>
    public DateTime LastSyncDate { get; set; }

    public string? TelegramBotToken { get; set; }

    public long TelegramAdminChatId { get; set; }

    public bool TelegramExtendedLog { get; set; }
}