using System;
using XProxy.Domain;
using XProxy.DAL;
namespace XProxy.Services
{
    public static class UserSettingsMapper
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
                XLombardDealTypeId = userSettingsEntity.XLombardFilialId
            };

    }
}

