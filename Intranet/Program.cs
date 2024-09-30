using Intranet.Controllers;
using Intranet.Data;
using Intranet.Hubs;
using Intranet.MiddlewareExtensions;
using Intranet.Models;
using Intranet.SubscribeTableDependencies;
using Intranet.ViewModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Configuration;
using NuGet.Protocol;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton
    );

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// DI
builder.Services.AddSingleton<ApplicationDbContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ConnectionHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();
builder.Services.AddSingleton<SubscribePostTableDependency>();
builder.Services.AddSingleton<SubscribeCommentTableDependency>();
builder.Services.AddSingleton<SubscribeLikeTableDependency>();
builder.Services.AddSingleton<SubscribeAttachmentTableDependency>();
builder.Services.AddSingleton<SubscribeDirectoryGroupTableDependency>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(365);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    // User settings.
    options.User.RequireUniqueEmail = false;
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 1000000000;
});

builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 1000000000;
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    //Culturee settings
    options.SetDefaultCulture("en-GB");
    options.FallBackToParentUICultures = true;
    options.RequestCultureProviders.Clear();
});

var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
};

LogEventLevel LogLevel = LogEventLevel.Error;

builder.Host.UseSerilog((ctx, lc) => lc
    .MinimumLevel.Override("Microsoft", LogLevel)
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .WriteTo.File(string.Format("logs/log-{0}", DateTime.Now.ToString("yyyy-MM-dd")))
  );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCookiePolicy(cookiePolicyOptions);
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapHub<ConnectionHub>("/connectionHub");
app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribePostTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeCommentTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeLikeTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeAttachmentTableDependency>(connectionString);
app.UseSqlTableDependency<SubscribeDirectoryGroupTableDependency>(connectionString);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
