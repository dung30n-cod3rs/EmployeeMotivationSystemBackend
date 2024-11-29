namespace EmployeeMotivationSystem.API.Models;

public sealed record LoginUserRequestApiDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public sealed record LoginUserResponseApiDto
{
    public required string Token { get; init; }
    public required string RefreshToken { get; init; }
}