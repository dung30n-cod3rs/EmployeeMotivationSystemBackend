namespace EmployeeMotivationSystem.API.Models;

public sealed record RegisterUserRequestApiDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}

public sealed record RegisterUserResponseApiDto
{
    public required string Token { get; init; }
    public required string RefreshToken { get; init; }
}