using FriendsTown.Data;
using FriendsTown.Data.Repositories;
using FriendsTown.Transversal;
using FriendsTown.Web.Hubs;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddMemoryCache();
builder.Services.AddOpenApi();
builder.Services.AddSignalR();

string apiUrl = builder.Configuration.GetValue<string>("ApiUrl");
builder.Services.AddHttpClient("FriendsTownWebApi", client =>
{

    client.BaseAddress = new Uri(apiUrl);

});

builder.Services.AddDbContext<FriendsTownContext>(options => options
    .UseSqlServer(builder.Configuration
    .GetConnectionString("FriendsTown")), ServiceLifetime.Singleton);

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();
app.UseSession();
app.UseAuthorization();
app.MapHub<BoardHub>("/boardHub");
app.MapOpenApi();
app.MapScalarApiReference();

using (var scope = app.Services.CreateAsyncScope())
{
    var dbContext = scope.ServiceProvider
                         .GetRequiredService<FriendsTownContext>();
    dbContext.Database.EnsureCreated();
}

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

