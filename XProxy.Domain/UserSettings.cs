namespace XProxy.Domain;

/// <summary>
/// Card element, for working with BL
/// </summary>
public class UserSettings
{
    public long Id { get; set; }

    public string AV100Token { get; set; }

    public string XLombardAPIUrl { get; set; }

    public string XLombardToken { get; set; }

    public long XLombardFilialId { get; set; }

    public long XLombardDealTypeId { get; set; }

    public string XLombardSource { get; set; }

    public string? TelegramBotToken { get; set; }

    public long TelegramAdminChatId { get; set; }

    public bool TelegramExtendedLog { get; set; }
}