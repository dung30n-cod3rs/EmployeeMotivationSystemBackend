using EmployeeMotivationSystem.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.DAL;

public sealed class AppDbContext : DbContext
{
    public DbSet<CompaniesUser> CompaniesUsers { get; init; }
    public DbSet<CompaniesUsersFilials> CompaniesUsersFilials { get; init; }
    public DbSet<CompaniesUsersMetric> CompaniesUsersMetrics { get; init; }
    public DbSet<Company> Companies { get; init; }
    public DbSet<Filial> Filials { get; init; }
    public DbSet<Metric> Metrics { get; init; }
    public DbSet<Position> Positions { get; init; }
    public DbSet<RefreshToken> RefreshTokens { get; init; }
    public DbSet<User> Users { get; init; }

    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
}