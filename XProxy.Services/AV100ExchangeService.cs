using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services
{
    public class AV100ExchangeService : IAV100ExchangeService
    {
        private const string JsonContentType = "application/json";
        private readonly ExchangeServiceOptions _options;
        private readonly UserSettings _userSettings;
        private readonly AV100Filter _filter;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IXProxyOptions _xOptions;

        public AV100ExchangeService(
            ExchangeServiceOptions options,
            UserSettings userSettings,
            AV100Filter filter,
            IHttpClientFactory httpClientFactory,
            IXProxyOptions xProxyOptions)
        {
            _options = options;
            _userSettings = userSettings;
            _filter = filter;
            _httpClientFactory = httpClientFactory;
            _xOptions = xProxyOptions;
        }

        private static StringContent CreateContent(string content)
        {
            return new StringContent(content, Encoding.UTF8, JsonContentType);
        }

        private StringBuilder ParamsBuilder(string baseUrl = default, long yearStart = default, long yearEnd = default,
            long priceStart = default, long priceEnd = default, long distanceStart = default, long distanceEnd = default,
            long[] regions = default, long[] sources = default, long fromId = default, long toId = default)
        {
            var sb = new StringBuilder(baseUrl);

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
                sb.Append($"&listregionid=.{string.Join(".", regions)}.");

            if (sources.Length > 0)
                sb.Append($"&source=.{string.Join(".", sources)}.");

            if (fromId != 0)
                sb.Append($"&fromId={fromId}");

            if (toId != 0)
                sb.Append($"&toId={toId}");

            return sb;
        }

        private async Task<TResponse> PostAsync<TResponse>(string url, CancellationToken token = default)
        {
            var client = _httpClientFactory.CreateClient(_xOptions.HttpClientName);
            var result = await client.PostAsync(url, CreateContent(string.Empty), token);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync(token);
                try
                {
                    return JsonConvert.DeserializeObject<TResponse>(content);
                }
                catch (Exception ex)
                {
                    throw new Exception("PostAcync result convert exception", ex);
                }
            }
            throw new Exception("PostAcync don't handle error http status code");
        }

        /// <summary>
        /// Get api url for checking settings
        /// </summary>
        public async Task<string> AV100RequestString(CancellationToken token = default)
        {
            return ParamsBuilder(
                _options.AV100RequestUrl(_userSettings.AV100Token, _options.OfferListOperation, _options.OfferListCountCommand),
                _filter.YearStart, _filter.YearEnd, _filter.PriceStart, _filter.PriceEnd, _filter.DistanceStart, _filter.DistanceEnd,
                _filter.RegionExternalIds, _filter.SourceExternalIds).ToString();
        }

        public async Task<long> AV100ReuestListCount(long fromId, long toId, CancellationToken token = default)
        {
            var url = ParamsBuilder(
                _options.AV100RequestUrl(_userSettings.AV100Token, _options.OfferListOperation, _options.OfferListCountCommand),
                _filter.YearStart, _filter.YearEnd, _filter.PriceStart, _filter.PriceEnd, _filter.DistanceStart, _filter.DistanceEnd,
                _filter.RegionExternalIds, _filter.SourceExternalIds, fromId, toId).ToString();

            var result = await PostAsync<AV100ResponseCount>(url, token);
                
            return result.Count;
        }
    }
}