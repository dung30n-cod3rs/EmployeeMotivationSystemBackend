namespace EmployeeMotivationSystem.API.Models.Base;

public sealed record PositionApiDto
{
    public required DateTime CreationDate { get; init; }
    public required string Name { get; init; }
    public required int Weight { get; init; }
    
    public required CompanyApiDto Company { get; init; }
}