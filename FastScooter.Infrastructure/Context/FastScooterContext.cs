using Microsoft.EntityFrameworkCore;

using FastScooter.Infrastructure.Models;

namespace FastScooter.Infrastructure.Context;

public class FastScooterContext : DbContext
{
    // Constructors
    public FastScooterContext()
    {
    }
    public FastScooterContext(DbContextOptions<FastScooterContext> options) : base(options)
    {
    }
    
    // DbSets for the entities
    public DbSet<User> Users { get; init; }
    public DbSet<Scooter> Scooters { get; init; }
    public DbSet<Rent> Rents { get; init; }
    
    public DbSet<Favorites> Favorites { get; init; }
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
      //  builder.Entity<User>()("Tutorial");

    }
}