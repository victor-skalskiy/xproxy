using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Domain;
using Microsoft.EntityFrameworkCore;

namespace XProxy.Services;

public class FiltersService : IFiltersService
{
    private readonly DataContext _context;

    public FiltersService(DataContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Getting list of filters, for index page
    /// </summary>
    public async Task<List<AV100FilterItem>> GetFiltersAsync(CancellationToken token = default)
    {
        return await _context.AV100Filters.Select(x => Mapper.GetFilterItem(x)).ToListAsync();
    }

    /// <summary>
    /// Getting specific filter item for edit page or api requests
    /// </summary>
    public async Task<AV100Filter> GetFilterAsync(long id, CancellationToken token = default)
    {
        var filterEntity = await _context.AV100Filters.Where(x => x.Id == id)
            .Include(s => s.Sources)
            .Include(r => r.Regions)
            .FirstOrDefaultAsync(token);

        var regions = await _context.AV100Regions.Select(x => Mapper.GetRegion(x)).ToListAsync();
        var sources = await _context.AV100Sources.Select(x => Mapper.GetSource(x)).ToListAsync();        

        if (filterEntity is null)
            throw new Exception($"Can't get AV100FilterEntity by id = {id}");

        return Mapper.GetFilter(filterEntity, regions, sources);
    }

    /// <summary>
    /// Creating filter
    /// </summary>
    public async Task<AV100Filter> CreateFilterAsync(long yearStart, long yearEnd, long priceStart, long priceEnd, long distanceStart,
        long distanceEnd, long carCount, long phoneCount, List<long> regionIds, List<long> sourceIds, CancellationToken token = default)
    {
        var filter = Mapper.FillFilterEntity(new AV100FilterEntity(), yearStart, yearEnd, priceStart, priceEnd, distanceStart, distanceEnd,
            carCount, phoneCount, regionIds, sourceIds);

        filter.Regions = _context.AV100Regions.Where(x => regionIds.Contains(x.Id)).ToList();
        filter.Sources = _context.AV100Sources.Where(x => sourceIds.Contains(x.Id)).ToList();

        _context.AV100Filters.Add(filter);
        await _context.SaveChangesAsync(token);

        return Mapper.GetFilter(filter, null, null);
    }

    /// <summary>
    /// Updating filter
    /// </summary>    
    public async Task<AV100Filter> UpdateFilterAsync(long id, long yearStart, long yearEnd, long priceStart, long priceEnd, long distanceStart,
        long distanceEnd, long carCount, long phoneCount, List<long> regionIds, List<long> sourceIds, CancellationToken token = default)
    {
        var filterEntity = await _context.AV100Filters
            .Include(x => x.Regions)
            .Include(x => x.Sources)
            .Where(x => x.Id == id).FirstOrDefaultAsync(token);

        if (filterEntity is null)
            throw new Exception($"Can't get AV100FilterEntity by id = {id}");

        filterEntity = Mapper.FillFilterEntity(filterEntity, yearStart, yearEnd, priceStart, priceEnd, distanceStart, distanceEnd,
            carCount, phoneCount, regionIds, sourceIds);

        filterEntity.Regions = _context.AV100Regions.Where(x => regionIds.Contains(x.Id)).ToList();
        
        filterEntity.Sources = _context.AV100Sources.Where(x => sourceIds.Contains(x.Id)).ToList();

        _context.AV100Filters.Update(filterEntity);
        await _context.SaveChangesAsync(token);

        return Mapper.GetFilter(filterEntity, null, null);
    }

    /// <summary>
    /// Get list of source items for controls
    /// </summary>
    public async Task<ICollection<AV100Source>> GetSourcesAsync(CancellationToken token = default)
    {
        return await _context.AV100Sources.Select(x => Mapper.GetSource(x)).ToListAsync();
    }

    /// <summary>
    /// Create Source 
    /// </summary>
    public async Task<AV100Source> CreateSourceAsync(long id, string name, CancellationToken token = default)
    {
        var sourceEntity = new AV100SourceEntity() { Id = id, Name = name };

        _context.AV100Sources.Add(sourceEntity);
        await _context.SaveChangesAsync(token);

        return Mapper.GetSource(sourceEntity);
    }

    /// <summary>
    /// Get list of Region for controls
    /// </summary>
    public async Task<ICollection<AV100Region>> GetRegionsAsync(CancellationToken token = default)
    {
        return await _context.AV100Regions.Select(x => Mapper.GetRegion(x)).ToListAsync();
    }

    /// <summary>
    /// Create Region 
    /// </summary>
    public async Task<AV100Region> CreateRegionAsync(long id, string name, CancellationToken token = default)
    {
        var regionEntity = new AV100RegionEntity() { Id = id, Name = name };

        _context.AV100Regions.Add(regionEntity);
        await _context.SaveChangesAsync(token);

        return Mapper.GetRegion(regionEntity);
    }

    /// <summary>
    /// Create lot of Regions
    /// </summary>
    public async Task<ICollection<AV100Region>> CreateRegionsAsync(Dictionary<long, string> data, CancellationToken token = default)
    {
        var exists = await _context.AV100Regions.Select(x => Mapper.GetRegion(x)).ToDictionaryAsync(z => z.RegionId, z => z.Name);
        foreach (var item in data)
        {
            if (exists.ContainsKey(item.Key))
                continue;

            _context.AV100Regions.Add(new AV100RegionEntity { AV100RegionId = item.Key, Name = item.Value });
        }
        await _context.SaveChangesAsync(token);

        return await _context.AV100Regions.Select(x => Mapper.GetRegion(x)).ToArrayAsync();
    }

    /// <summary>
    /// Create a log of Sources
    /// </summary>
    public async Task<ICollection<AV100Source>> CreateSourcesAsync(Dictionary<long, string> data, CancellationToken token = default)
    {
        var exists = await _context.AV100Sources.Select(x => Mapper.GetSource(x)).ToDictionaryAsync(z => z.SourceId, z => z.Name);
        foreach (var item in data)
        {
            if (exists.ContainsKey(item.Key))
                continue;

            _context.AV100Sources.Add(new AV100SourceEntity { AV100SourceId = item.Key, Name = item.Value });
        }
        await _context.SaveChangesAsync(token);

        return await _context.AV100Sources.Select(x => Mapper.GetSource(x)).ToArrayAsync();
    }
}