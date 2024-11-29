namespace EmployeeMotivationSystem.DAL.Models;

public sealed record RefreshToken : BaseModel
{
    public required int UserId { get; init; }
    public required string Token { get; init; }
    public required DateTime ExpiresAt { get; init; }
}