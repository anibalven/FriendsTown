using FriendsService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FriendsDbContext>(options =>
    options.UseSqlServer(builder.Configuration
           .GetConnectionString("FriendsDB")));

var app = builder.Build();

app.MapGet("/friend-events", async (FriendsDbContext db) =>
{
    var sql = @"
        SELECT f.Name, e.Date_Value, a.Name AS Activity
        FROM Friends f
        INNER JOIN Events e ON f.Id = e.OrganizerId
        INNER JOIN Activities a ON e.ActivityTypeId = a.Id";

    var result = await db.FriendActivities.FromSqlRaw(sql)
                                      .ToListAsync();
    return Results.Ok(result);
});

app.Run();
