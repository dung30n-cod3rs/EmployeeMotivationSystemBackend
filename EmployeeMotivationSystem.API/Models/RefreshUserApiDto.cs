namespace EmployeeMotivationSystem.API.Models;

public sealed record RefreshUserResponseApiDto
{
    public required string Token { get; init; }
    public required string RefreshToken { get; init; }
}