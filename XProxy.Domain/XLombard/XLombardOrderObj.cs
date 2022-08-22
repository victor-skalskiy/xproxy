using System;
using System.ComponentModel.DataAnnotations;

namespace XProxy.Domain;

/// <summary>
/// ClientPhone, Source, DealTypeId, FilialId
/// </summary>
public class XLombardOrderObj
{
    public XLombardOrderObj()
    {
        AdditionalFilials = new List<long>();
        ClientName = "Новый лид";
    }

    /// <summary>
    /// число, код филиала в который должна поступить заявка
    /// </summary>
    [Required]
    public long FilialId { get; set; }

    /// <summary>
    /// массив кодов филиалов (чисел), сотрудники которых тоже должны иметь доступ к данной заявке
    /// </summary>
    public List<long> AdditionalFilials { get; set; }

    /// <summary>
    /// число, код услуги (числовой код смотрим в конфигураторе для каждого типа услуги на вкладке Базовые справочники)
    /// </summary>
    [Required]
    public long DealTypeId { get; set; }

    /// <summary>
    /// строка, источник заявки (например адрес сайта без http)
    /// </summary>
    [Required]
    public string Source { get; set; }

    /// <summary>
    /// число в строковом представлении, код партнера
    /// </summary>
    public string PartnerCode { get; set; }

    /// <summary>
    /// Телефон клиента в формате 70000000000 (11 цифр),
    /// символ “+” перед номером также является допустимым (+70000000000)
    /// </summary>
    [Required]
    public string ClientPhone { get; set; }

    public string ClientMail { get; set; }

    public string TelegramAccount { get; set; }

    public string ClientName { get; set; }

    /// <summary>
    /// строка, значение добавляется в ленту событий, недоступно для изменения сотрудниками
    /// </summary>
    public string ClientComment { get; set; }


    /// <summary>
    /// Если значение Utm_source не передается в url при переходе клиента на сайт
    /// (например пользователь зашел с органики yandex),
    /// то в качестве Utm_source необходимо передать значение refferrer (например, yandex.ru или google.com),
    /// при этом значения полей Utm_campaign, Utm_medium можно не передавать.
    /// </summary>
    public string Utm_source { get; set; }
    public string Utm_campaign { get; set; }
    public string Utm_medium { get; set; }
    public string Utm_term { get; set; }
    public string Utm_content { get; set; }
    public string Utm_position_type { get; set; }
    public string Utm_device_type { get; set; }
    public string Utm_region_name { get; set; }

    /// <summary>
    /// строка, код сессии клиента, в рамках которой была создана текущая заявка.
    /// Также может быть проинициализировано кодом заявки во внешней системе - например, у лидогенератора
    /// </summary>
    public string SessionId { get; set; }

    public string AdvertClientId_1 { get; set; }

    public string AdvertClientId_2 { get; set; }

    public string RefUrl { get; set; }

    /// <summary>
    /// массив ссылок для скачивания изображений, которые необходимо прикрепить к заявке,
    /// изображение обязательно должно быть доступно методом Get по http или https.
    /// Элементом массива является строка, содержащая полный URL для скачивания изображения
    /// </summary>
    public List<object> ExternalImages { get; set; }

    /// <summary>
    /// набор полей (приведены для примера), соответствующих выбранному DealTypeid
    /// </summary>
    public List<XLombardOrderObjField> Fields { get; set; }
}