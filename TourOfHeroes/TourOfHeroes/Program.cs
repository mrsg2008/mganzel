using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(configure => configure
    .ClearProviders()
    .AddConsole()
    .AddDebug());

builder.Services.AddRazorPages();
builder.Services.AddScoped<IHeroService, HeroService>();
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddScoped<InMemoryDataService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.Configure<CookieTempDataProviderOptions>(options => options.Cookie.IsEssential = true);

var app = builder.Build();

app.UseSession();

app.MapRazorPages();

app.UseDeveloperExceptionPage();

app.Run();
