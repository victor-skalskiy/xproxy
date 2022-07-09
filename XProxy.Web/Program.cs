using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services
    .AddDbContext<DataContext>(options =>
    {
        options
            .UseNpgsql(connectionString);
    })
    .AddScoped<ISettingsService, SettingsService>()
    .AddHangfire(hangfire =>
        hangfire
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage())
    .AddControllersWithViews();

builder.Services.AddHttpClient("MyBaseClient", client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});


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

app.UseHangfireDashboard();

app.Run();

