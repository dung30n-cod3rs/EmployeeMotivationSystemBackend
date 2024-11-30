using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Positions;

public sealed record UpdatePositionRequestApiDto
{
    public required int PositionId { get; init; }
    
    public required string Name { get; init; }
    public required int Weight { get; init; }
    
    public required int CompanyId { get; init; }
}

public sealed record UpdatePositionResponseApiDto
{
    public required PositionApiDto Item { get; init; }
}