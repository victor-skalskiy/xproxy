using System.ComponentModel.DataAnnotations;

namespace XProxy.Web.Models;

public class SettingsEditModel
{
    [Display(Name = "API Token"), Required(ErrorMessage = "field is required")]
    public string Av100Token { get; set; }

    [Display(Name = "API url"), Required(ErrorMessage = "field is required")]
    public string XLombardAPIUrl { get; set; }

    [Display(Name = "API Token"), Required(ErrorMessage = "field is required")]
    public string XLombardToken { get; set; }

    [Display(Name = "Filial Id"), Required(ErrorMessage = "field is required")]
    public long XLombardFilialId { get; set; }

    [Display(Name = "Deal type Id"), Required(ErrorMessage = "field is required")]
    public long XLombardDealTypeId { get; set; }

    [Display(Name = "Source"), Required(ErrorMessage = "field is required")]
    public string XLombardSource { get; set; }

    [Display(Name = "TelegramBotToken"), Required(ErrorMessage = "field is required")]
    public string TelegramBotToken { get; set; }

    [Display(Name = "TelegramAdminChatId"), Required(ErrorMessage = "field is required")]
    public long TelegramAdminChatId { get; set; }

    [Display(Name = "TelegramExtendedLog"), Required(ErrorMessage = "field is required")]
    public bool TelegramExtendedLog { get; set; }
}