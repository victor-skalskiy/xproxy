using System;
using Newtonsoft.Json;

namespace XProxy.Domain;

/// <summary>
/// Class-answer from AV100 Offer List
/// </summary>
public class AV100ResponseOffer : AV100ResponseBase
{
    public AV100ResponseOffer() { Result = new AV100ResponseOfferResult(); }

    [JsonProperty("result")]
    public AV100ResponseOfferResult Result { get; set; }
}

/// <summary>
/// Field AV100ResponseOffer
/// </summary>
public class AV100ResponseOfferResult
{
    public AV100ResponseOfferResult() { ListOffer = new List<AV100ResponseOfferResultRow>(); }

    [JsonProperty("ListOffer")]
    public List<AV100ResponseOfferResultRow> ListOffer { get; set; }
}

/// <summary>
/// Row data of AV100ResponseOffer
/// </summary>
public class AV100ResponseOfferResultRow
{
    public AV100ResponseOfferResultRow() { }

    /// <summary>
    /// Наименование ТС
    /// </summary>
    [JsonProperty("Title")]
    public string Title { get; set; }

    /// <summary>
    /// ID объявления на источнике
    /// </summary>
    [JsonProperty("ID")]
    public int ID { get; set; }

    /// <summary>
    /// Год выпуска ТС
    /// </summary>
    [JsonProperty("Year")]
    public int Year { get; set; }

    /// <summary>
    /// Ссылка на объявление
    /// </summary>
    [JsonProperty("Url")]
    public string Url { get; set; }

    /// <summary>
    /// Марка ТС
    /// </summary>
    [JsonProperty("Marka")]
    public string Marka { get; set; }

    /// <summary>
    /// Модель ТС
    /// </summary>
    [JsonProperty("Model")]
    public string Model { get; set; }

    /// <summary>
    /// Дата объявления
    /// </summary>
    [JsonProperty("Credate")]
    public string Credate { get; set; }

    /// <summary>
    /// Тип коробки передач,
    /// 1 - Механическая, 2 - Автомат, 3 - Робот, 4 - Вариатор
    /// </summary>
    [JsonProperty("Kpp")]
    public int Kpp { get; set; }

    /// <summary>
    /// Объем двигателя ТС
    /// </summary>
    [JsonProperty("Volume")]
    public string Volume { get; set; }

    /// <summary>
    /// Тип кузова ТС
    /// </summary>
    [JsonProperty("Body")]
    public string Body { get; set; }

    /// <summary>
    /// Цена ТС указанная в объявлении
    /// </summary>
    [JsonProperty("Price")]
    public int Price { get; set; }

    /// <summary>
    /// Признак наличия телефона в объявлении
    /// </summary>
    [JsonProperty("PhoneExist")]
    public int PhoneExist { get; set; }

    /// <summary>
    /// Источник объявления
    /// </summary>
    [JsonProperty("Source")]
    public int Source { get; set; }

    /// <summary>
    /// Город в котором продают/продавали ТС
    /// </summary>
    [JsonProperty("City")]
    public string City { get; set; }

    /// <summary>
    /// Описание ТС в объявлении
    /// </summary>
    [JsonProperty("Descr")]
    public string Descr { get; set; }

    /// <summary>
    /// Количество разных машин с номера
    /// </summary>
    [JsonProperty("CountCar")]
    public int CountCar { get; set; }

    /// <summary>
    /// Количество объявлений с номера
    /// </summary>
    [JsonProperty("CountOffer")]
    public int CountOffer { get; set; }

    /// <summary>
    /// Руль ТС, 1 - левый, 2 - правый
    /// </summary>
    [JsonProperty("Wheel")]
    public int Wheel { get; set; }

    /// <summary>
    /// Признак битого авто
    /// </summary>
    [JsonProperty("Remont")]
    public int Remont { get; set; }

    /// <summary>
    /// Пробег ТС указанный в объявлении
    /// </summary>
    [JsonProperty("Distance")]
    public int Distance { get; set; }

    /// <summary>
    /// Кол-во просмотров объявления
    /// </summary>
    [JsonProperty("View")]
    public int View { get; set; }

    /// <summary>
    /// Признак наличия фотографий ТС в объявлении
    /// </summary>
    [JsonProperty("FotoExist")]
    public int FotoExist { get; set; }

    /// <summary>
    /// Признак подменного номера
    /// </summary>
    [JsonProperty("IsFakePhone")]
    public int IsFakePhone { get; set; }

    /// <summary>
    /// Строка с ссылками на изображения,
    /// !!!! ссылки разеделены запятой и пробелом
    /// </summary>
    [JsonProperty("Images")]
    public string Images { get; set; }

    /// <summary>
    /// Изменение цены
    /// </summary>
    [JsonProperty("Delta")]
    public int Delta { get; set; }

    /// <summary>
    /// VIN номер ТС указанный в объявлении
    /// </summary>
    [JsonProperty("Vin")]
    public string Vin { get; set; }

    [JsonProperty("Phone")]
    public string Phone { get; set; }

    [JsonProperty("Contactface")]
    public string Contactface { get; set; }
}