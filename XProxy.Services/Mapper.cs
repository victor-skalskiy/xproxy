using System;
using XProxy.Domain;
using XProxy.DAL;
namespace XProxy.Services;

public static class Mapper
{
    public static UserSettingsEntity FillSettingsEntity(UserSettingsEntity userSettingsEntity, long updateInterval, string av100Token,
        string xLombardAPIUrl, string xLombardToken, long xLombardFilialId, long xLombardDealTypeId, string xLombardSource)
    {
        userSettingsEntity.AV100Token = av100Token;
        userSettingsEntity.UpdateInterval = updateInterval;
        userSettingsEntity.XLombardAPIUrl = xLombardAPIUrl;
        userSettingsEntity.XLombardToken = xLombardToken;
        userSettingsEntity.ModifyDate = DateTime.UtcNow;
        userSettingsEntity.XLombardFilialId = xLombardFilialId;
        userSettingsEntity.XLombardSource = xLombardSource;
        userSettingsEntity.XLombardDealTypeId = xLombardFilialId;

        return userSettingsEntity;
    }
    public static UserSettings GetUserSettings(UserSettingsEntity userSettingsEntity)
        => new UserSettings
        {
            AV100Token = userSettingsEntity.AV100Token,
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
    public static AV100Filter GetFilter(AV100FilterEntity aV100FilterEntity, List<AV100Region> aV100Regions, List<AV100Source> aV100Sources)
        => new AV100Filter
        {
            Id = aV100FilterEntity.Id,
            YearStart = aV100FilterEntity.YearStart,
            YearEnd = aV100FilterEntity.YearEnd,
            PriceStart = aV100FilterEntity.PriceStart,
            PriceEnd = aV100FilterEntity.PriceEnd,
            DistanceStart = aV100FilterEntity.DistanceStart,
            DistanceEnd = aV100FilterEntity.DistanceEnd,
            CarCount = aV100FilterEntity.CarCount,
            PhoneCount = aV100FilterEntity.PhoneCount,
            AllRegions = aV100Regions,
            AllSources = aV100Sources,
            //TODO почему-то выбранный энтити с нуловыми связями
            RegionIds = aV100FilterEntity.Regions?.Select(x => x.Id).ToArray() ?? Array.Empty<long>(),
            SourceIds = aV100FilterEntity.Sources?.Select(x => x.Id).ToArray() ?? Array.Empty<long>(),
            RegionExternalIds = aV100FilterEntity.Regions?.Select(x => x.AV100RegionId).ToArray() ?? Array.Empty<long>(),
            SourceExternalIds = aV100FilterEntity.Sources?.Select(x => x.AV100SourceId).ToArray() ?? Array.Empty<long>()
        };
    public static AV100FilterItem GetFilterItem(AV100FilterEntity aV100FilterEntity)
        => new AV100FilterItem
        {
            Id = aV100FilterEntity.Id,
            YearStart = aV100FilterEntity.YearStart,
            YearEnd = aV100FilterEntity.YearEnd,
            PriceStart = aV100FilterEntity.PriceStart,
            PriceEnd = aV100FilterEntity.PriceEnd,
            DistanceStart = aV100FilterEntity.DistanceStart,
            DistanceEnd = aV100FilterEntity.DistanceEnd,
        };
    public static AV100Source GetSource(AV100SourceEntity aV100SourceEntity)
        => new AV100Source
        {
            Id = aV100SourceEntity.Id,
            SourceId = aV100SourceEntity.AV100SourceId,
            Name = aV100SourceEntity.Name
        };
    public static AV100Source GetSource(AV100ResponseSourceResultRow aV100ResponseSourceResultRow)
        => new AV100Source
        {
            SourceId = aV100ResponseSourceResultRow.SourceId,
            Name = aV100ResponseSourceResultRow.SourceName
        };
    public static AV100Region GetRegion(AV100RegionEntity aV100RegionEntity)
        => new AV100Region
        {
            Id = aV100RegionEntity.Id,
            RegionId = aV100RegionEntity.AV100RegionId,
            Name = aV100RegionEntity.Name
        };
    public static AV100Region GetRegion(AV100ResponseRegionResultRow aV100ResponseRegionResultRow)
        => new AV100Region
        {
            RegionId = aV100ResponseRegionResultRow.RegionId,
            Name = aV100ResponseRegionResultRow.RegionName
        };
    public static AV100FilterEntity FillFilterEntity(AV100FilterEntity entity, long yearStart, long yearEnd, long priceStart, long priceEnd, long distanceStart,
        long distanceEnd, long carCount, long phoneCount, List<long> regionIds, List<long> sourceIds)
    {
        entity.YearStart = yearStart;
        entity.YearEnd = yearEnd;
        entity.PriceStart = priceStart;
        entity.PriceEnd = priceEnd;
        entity.DistanceStart = distanceStart;
        entity.DistanceEnd = distanceEnd;
        entity.CarCount = carCount;
        entity.PhoneCount = phoneCount;
        entity.ModifyDate = DateTime.UtcNow;
        entity.PhoneCount = phoneCount;

        return entity;
    }
}