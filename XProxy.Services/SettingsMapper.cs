using System;
using XProxy.Domain;
using XProxy.DAL;
namespace XProxy.Services
{
    public static class SettingsMapper
    {
        public static UserSettingsEntity GetEntity(UserSettings userSettings) =>
            new UserSettingsEntity
            {

            };

        public static UserSettings GetUserSettings(UserSettingsEntity userSettingsEntity)
            => new UserSettings
            {
                Av100Token = userSettingsEntity.AV100Token,
                Id = userSettingsEntity.Id,
                XLombardAPIUrl = userSettingsEntity.XLombardAPIUrl,
                XLombardToken = userSettingsEntity.XLombardToken,
                UpdateInterval = userSettingsEntity.UpdateInterval,
                XLombardFilialId = userSettingsEntity.XLombardFilialId,
                XLombardSource = userSettingsEntity.XLombardSource,
                XLombardDealTypeId = userSettingsEntity.XLombardDealTypeId
            };
        public static UserSettingsItem GetUserSettingsItem(UserSettingsEntity userSettingsEntity)
            => new UserSettingsItem
            {
                Av100Token = userSettingsEntity.AV100Token,
                Id = userSettingsEntity.Id,
                XLombardAPIUrl = userSettingsEntity.XLombardAPIUrl,
                XLombardToken = userSettingsEntity.XLombardToken,
                UpdateInterval = userSettingsEntity.UpdateInterval
            };
        public static XLombardOrderObj GetXLombardOrderObj(UserSettings userSettings)
            => new XLombardOrderObj
            {
                Source = userSettings.XLombardSource,
                DealTypeId = userSettings.XLombardDealTypeId,
                FilialId = userSettings.XLombardFilialId,
            };

        public static Av100Filter GetFilter(AV100FilterEntity aV100FilterEntity)
            => new Av100Filter
            {
                YearStart = aV100FilterEntity.YearStart,
                YearEnd = aV100FilterEntity.YearEnd,
                PriceStart = aV100FilterEntity.PriceStart,
                PriceEnd = aV100FilterEntity.PriceEnd,
                DistanceStart = aV100FilterEntity.DistanceStart,
                DistanceEnd = aV100FilterEntity.DistanceEnd,
                CarCount = aV100FilterEntity.CarCount,
                PhoneCount = aV100FilterEntity.PhoneCount,
                Id = aV100FilterEntity.Id,
                UserSettingsId = aV100FilterEntity.UserSettingsEntityId
                //TODO реализовать заполнение RegionId
            };
        public static Av100FilterItem GetFilterItem(AV100FilterEntity aV100FilterEntity)
            => new Av100FilterItem
            {
                YearStart = aV100FilterEntity.YearStart,
                YearEnd = aV100FilterEntity.YearEnd,
                PriceStart = aV100FilterEntity.PriceStart,
                PriceEnd = aV100FilterEntity.PriceEnd,
                DistanceStart = aV100FilterEntity.DistanceStart,
                DistanceEnd = aV100FilterEntity.DistanceEnd,
                CarCount = aV100FilterEntity.CarCount,
                PhoneCount = aV100FilterEntity.PhoneCount,
            };

    }
}

