namespace EmployeeMotivationSystem.API.Middleware.Exceptions;

public sealed record ExceptionCatcherMiddlewareModel
{
    public DateTime CreationDate { get; init; } = DateTime.UtcNow;
    public required string ErrorMessage { get; init; }
}