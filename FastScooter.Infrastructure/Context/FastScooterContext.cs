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
}