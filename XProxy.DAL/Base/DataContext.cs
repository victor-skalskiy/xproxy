using System;
using Microsoft.EntityFrameworkCore;

namespace XProxy.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<AV100Filter> _AV100Filters { get; set; }
        public DbSet<AV100Region> AV100Regions { get; set; }
        public DbSet<AV100Settings> AV100Settings { get; set; }
        public DbSet<AV100Source> AV100Sources { get; set; }

        public DbSet<XSettings> XSettings { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
    }
}

