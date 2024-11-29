namespace EmployeeMotivationSystem.API.Models.Users;

public class ChangeUserPasswordRequestApiDto
{
    public required int Id { get; init; }
    public required string Password { get; init; }
    public required string RepeatPassword { get; init; }
}