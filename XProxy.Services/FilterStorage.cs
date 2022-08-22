using Microsoft.EntityFrameworkCore;
using XProxy.DAL;
using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services;

public sealed class FilterStorage : IFilterStorage
{
    private readonly DataContext _context;
    private readonly IFiltersService _filtersService;
    
    public FilterStorage(DataContext context, IFiltersService filtersService)
    {
        _context = context;
        _filtersService = filtersService;
    }

    public async Task<AV100Filter> GetFilterAsync(long filterId, CancellationToken token = default)
    {
        if (_context.AV100Filters.Count() == 0)
            await _filtersService.CreateTempFilterAsync();

        var filterEntity = await _context.AV100Filters.Where(x => x.Id == filterId)
            .Include(s => s.Sources)
            .Include(r => r.Regions)
            .FirstOrDefaultAsync(token);

        if (filterEntity == null)
        {
            throw new Exception($"AV100 Filter was not found by id '{filterId}'");
        }

        var regions = await _context.AV100Regions.Select(x => Mapper.GetRegion(x)).ToListAsync();
        var sources = await _context.AV100Sources.Select(x => Mapper.GetSource(x)).ToListAsync();

        return Mapper.GetFilter(filterEntity, regions, sources);
    }
}