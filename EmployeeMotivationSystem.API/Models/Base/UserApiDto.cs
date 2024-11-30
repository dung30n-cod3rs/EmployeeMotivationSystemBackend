namespace EmployeeMotivationSystem.API.Models.Base;

public sealed record UserApiDto
{
    public required int Id { get; init; }
    public required DateTime CreationDate { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    
    public required int CompanyId { get; init; }
}