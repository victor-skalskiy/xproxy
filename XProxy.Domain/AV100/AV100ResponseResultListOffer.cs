using System;
using Newtonsoft.Json;

namespace XProxy.Domain;

public class AV100ResponseResultListOffer
{
    public AV100ResponseResultListOffer() { }

    [JsonProperty(PropertyName = "Title")]
    public string Title { get; set; }

    [JsonProperty(PropertyName = "ID")]
    public int Id { get; set; }

    [JsonProperty(PropertyName = "Year")]
    public int Year { get; set; }

    [JsonProperty(PropertyName = "Url")]
    public string Url { get; set; }

    [JsonProperty(PropertyName = "Credate")]
    public string Credate { get; set; }

    [JsonProperty(PropertyName = "Price")]
    public int Price { get; set; }

    [JsonProperty(PropertyName = "City")]
    public string City { get; set; }

    [JsonProperty(PropertyName = "Distance")]
    public int Distance { get; set; }

    [JsonProperty(PropertyName = "Phone")]
    public string Phone { get; set; }

    [JsonProperty(PropertyName = "Contactface")]
    public string Contactface { get; set; }

    /*
    [JsonProperty(PropertyName = "Marka")]
    public string Marka { get; set; }

    [JsonProperty(PropertyName = "Model")]
    public string Model { get; set; }

    [JsonProperty(PropertyName = "Kpp")]
    public int Kpp { get; set; }

    [JsonProperty(PropertyName = "Volume")]
    public string Volume { get; set; }

    [JsonProperty(PropertyName = "Body")]
    public string Body { get; set; }

    [JsonProperty(PropertyName = "PhoneExist")]
    public int PhoneExist { get; set; }

    [JsonProperty(PropertyName = "Source")]
    public int Source { get; set; }

    [JsonProperty(PropertyName = "Descr")]
    public string Descr { get; set; }

    [JsonProperty(PropertyName = "CountCar")]
    public int CountCar { get; set; }

    [JsonProperty(PropertyName = "CountOffer")]
    public int CountOffer { get; set; }

    [JsonProperty(PropertyName = "Wheel")]
    public int Wheel { get; set; }

    [JsonProperty(PropertyName = "Remont")]
    public int Remont { get; set; }
 
    [JsonProperty(PropertyName = "View")]
    public int View { get; set; }

    [JsonProperty(PropertyName = "FotoExist")]
    public int FotoExist { get; set; }

    [JsonProperty(PropertyName = "IsFakePhone")]
    public int IsFakePhone { get; set; }

    [JsonProperty(PropertyName = "Images")]
    public string Images { get; set; }

    [JsonProperty(PropertyName = "Delta")]
    public int Delta { get; set; }

    [JsonProperty(PropertyName = "Vin")]
    public string Vin { get; set; }
    */
}