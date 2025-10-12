using Microsoft.EntityFrameworkCore;

namespace FriendsService;

public class FriendsDbContext : DbContext
{
    public FriendsDbContext(DbContextOptions<FriendsDbContext> options) 
        : base(options) { }

    public DbSet<FriendsDto> FriendActivities => Set<FriendsDto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FriendsDto>().HasNoKey();
    }
}

 