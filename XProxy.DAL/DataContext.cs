using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace XProxy.DAL;

public class DataContext : IdentityDbContext<IdentityUser>
{
    public DbSet<AV100FilterEntity> AV100Filters { get; set; }
    public DbSet<AV100RegionEntity> AV100Regions { get; set; }
    public DbSet<AV100SourceEntity> AV100Sources { get; set; }
    public DbSet<AV100RecordEntity> AV100Records { get; set; }
    public DbSet<UserSettingsEntity> UserSettings { get; set; }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}