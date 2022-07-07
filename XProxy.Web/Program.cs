using Microsoft.EntityFrameworkCore;
using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services
    .AddDbContext<DataContext>(options =>
    {
        options
            .UseNpgsql(connectionString,
        assebmly => { assebmly.MigrationsAssembly("XProxy.DataMigration"); });
    })
    .AddScoped<ISettingsService, SettingsService>()
    .AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Settings/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Settings}/{action=Index}/{id?}");

app.Run();

