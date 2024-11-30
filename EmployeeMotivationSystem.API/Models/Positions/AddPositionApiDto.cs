using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Positions;

public sealed record AddPositionRequestApiDto
{
    public required string Name { get; init; }
    public required int Weight { get; init; }
    
    public required int CompanyId { get; init; }
}

public sealed record AddPositionResponseApiDto
{
    public required PositionApiDto Item { get; init; }
}