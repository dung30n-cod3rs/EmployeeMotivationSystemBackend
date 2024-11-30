namespace EmployeeMotivationSystem.API.Models.Base;

public sealed record PositionApiDto
{
    public required DateTime PositionCreationDate { get; init; }
    public required string PositionName { get; init; }
    public required int PositionWeight { get; init; }
    
    public required DateTime CompanyCreationDate { get; init; }
    public required string CompanyName { get; init; }
}