using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Domain;
using Microsoft.EntityFrameworkCore;
using Polly;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace XProxy.Services;

public class FiltersService : IFiltersService
{
    private readonly DataContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly bool _uplink;

    public FiltersService(DataContext context, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _uplink = configuration.GetSection("AppSetting").GetSection("Uplink").Value == "True";
    }

    public Task<AV100Filter> CreateFilterAsync(long YearStart, string YearEnd, string PriceStart, string PriceEnd, long DistanceStart, long DistanceEnd, long CarCount, long PhoneCount, long Regionid, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<AV100Filter> GetFilterItemAsync(long id, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<AV100FilterItem>> GetFiltersAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }

    public Task<AV100Filter> UpdateFilterAsync(long id, long YearStart, string YearEnd, string PriceStart, string PriceEnd, long DistanceStart, long DistanceEnd, long CarCount, long PhoneCount, long Regionid, CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}