using FriendsTown.Data;
using FriendsTown.Transversal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<FriendsTownContext>(options => options
    .UseSqlServer(builder.Configuration
    .GetConnectionString("FriendsTown")), ServiceLifetime.Singleton);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();
app.UseAuthorization();

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

