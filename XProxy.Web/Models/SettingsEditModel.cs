using System;
using System.ComponentModel.DataAnnotations;

namespace XProxy.Web.Models
{
    public class SettingsEditModel
    {
        [Display(Name = "Токен AV100")]
        [Required(ErrorMessage = "Обязательно к заполнению")]
        public string Av100Token { get; set; }

        [Display(Name = "Путь к серверу XLombard")]
        public string XLAPIUrl { get; set; }

        [Display(Name = "Токен сервера XLombard")]
        public string XLToken { get; set; }

        [Display(Name = "Время обновления в минутах")]
        public long UserUpdateInterval { get; set; }
    }
}

