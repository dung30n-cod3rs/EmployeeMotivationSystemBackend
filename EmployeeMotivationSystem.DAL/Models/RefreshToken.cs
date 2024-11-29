namespace EmployeeMotivationSystem.DAL.Models;

public sealed record RefreshToken : BaseModel
{
    public int UserId { get; init; }
    public required User User { get; init; }
    
    public required string Token { get; init; }
    public required DateTime ExpiresAt { get; init; }
}