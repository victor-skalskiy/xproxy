using System.Text;
using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services
{
    public class AV100ExchangeService : IAV100ExchangeService
    {
        private readonly ExchangeServiceOptions _options;
        private readonly UserSettings _userSettings;
        private readonly AV100Filter _filter;

        public AV100ExchangeService(
            ExchangeServiceOptions options,
            UserSettings userSettings,
            AV100Filter filter)
        {
            _options = options;
            _userSettings = userSettings;
            _filter = filter;
        }

        private StringBuilder paramsBuilder(long yearStart = default, long yearEnd = default, long priceStart = default,
            long priceEnd = default, long distanceStart = default, long distanceEnd = default, long[] regions = default,
            long[] sources = default, long fromId = default, long toId = default)
        {
            var sb = new StringBuilder();

            if (yearStart != 0)
                sb.Append($"&yearStart={yearStart}");

            if (yearEnd != 0)
                sb.Append($"&yearEnd={yearEnd}");

            if (priceStart != 0)
                sb.Append($"&priceStart={priceStart}");

            if (priceEnd != 0)
                sb.Append($"&priceEnd={priceEnd}");

            if (distanceStart != 0)
                sb.Append($"&distanceStart={distanceStart}");

            if (distanceEnd != 0)
                sb.Append($"&distanceEnd={distanceEnd}");

            if (regions.Length > 0)
                sb.Append($"&listregionid={string.Join(".", regions)}");

            if (sources.Length > 0)
                sb.Append($"&source={string.Join(".", sources)}");

            if (fromId != 0)
                sb.Append($"&fromId={fromId}");

            if (toId != 0)
                sb.Append($"&toId={toId}");

            return sb;
        }

        /// <summary>
        /// Get api url for checking settings
        /// </summary>
        public async Task<string> AV100RequestString(CancellationToken token = default)
        {
            var sb = new StringBuilder(
                _options.AV100RequestUrl(_userSettings.AV100Token, _options.OfferListOperation, _options.OfferListCountCommand));

            if (_filter.YearStart != 0)
                sb.Append($"&yearStart={_filter.YearStart}");

            if (_filter.YearEnd != 0)
                sb.Append($"&yearEnd={_filter.YearEnd}");

            if (_filter.PriceStart != 0)
                sb.Append($"&priceStart={_filter.PriceStart}");

            if (_filter.PriceEnd != 0)
                sb.Append($"&priceEnd={_filter.PriceEnd}");

            if (_filter.DistanceStart != 0)
                sb.Append($"&distanceStart={_filter.DistanceStart}");

            if (_filter.DistanceEnd != 0)
                sb.Append($"&distanceEnd={_filter.DistanceEnd}");

            if (_filter.RegionIds.Length > 0)
                sb.Append($"&listregionid={string.Join(".", _filter.RegionIds)}");

            if (_filter.SourceIds.Length > 0)
                sb.Append($"&source={string.Join(".", _filter.SourceIds)}");

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromId"></param>
        /// <param name="toId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<long> AV100ReuestListCount(long fromId, long toId, CancellationToken token = default)
        {
            return 0;
        }
    }
}