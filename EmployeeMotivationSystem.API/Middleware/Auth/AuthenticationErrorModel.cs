namespace EmployeeMotivationSystem.API.Middleware.Auth;

public sealed record AuthenticationErrorModel
{
    public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    public required string ErrorMessage { get; init; }
}