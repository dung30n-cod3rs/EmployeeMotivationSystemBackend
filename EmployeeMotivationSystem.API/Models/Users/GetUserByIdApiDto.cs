namespace EmployeeMotivationSystem.API.Models.Users;

public sealed record GetUserByIdResponseApiDto
{
    public required DateTime CreationDate { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
}