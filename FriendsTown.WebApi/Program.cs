using FriendsTown.Data;
using FriendsTown.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddCors();
builder.Services.AddDbContext<FriendsTownContext>(options =>
    options.UseSqlServer("name=connectionStrings:FriendsTown"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed(origin => true)
            );

app.UseAuthorization();
app.MapControllers();

app.Run();
