using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using XProxy.DAL;
using XProxy.Interfaces;
using XProxy.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<DataContext>(options =>
    {
        options
            .UseNpgsql(
                connectionString,
                assebly =>
                    assebly.MigrationsAssembly("XProxy.DAL"));
    })
    .AddScoped<IUserSettingsStorage, UserSettingsStorage>()
    .AddScoped<IFilterStorage, FilterStorage>()
    .AddScoped<ISettingsService, SettingsService>()
    .AddScoped<IFiltersService, FiltersService>()
    .AddScoped<IExchangeServiceFactory, ExchangeServiceFactory>()
    .AddSingleton<IXProxyOptions, XProxyOptions>()
    .AddHangfire(hangfire =>
        hangfire
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseDefaultTypeSerializer()
            .UseMemoryStorage());


builder.Services.AddHttpClient("MyBaseClient", client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddTransient<IEmailSender, EmailService>()
    .AddDefaultIdentity<IdentityUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.SignIn.RequireConfirmedEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Settings/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Settings}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseHangfireDashboard();

app.Run();