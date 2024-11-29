using EmployeeMotivationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.DAL;

public sealed class AppDbContext : DbContext
{
    public DbSet<User> Users { get; init; }
    public DbSet<RefreshToken> RefreshTokens { get; init; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
}