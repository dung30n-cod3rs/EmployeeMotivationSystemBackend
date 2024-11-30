using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Metrics;

public sealed record UpdateMetricRequestApiDto
{
    public required int MetricId { get; init; }
    
    public required int PositionId { get; init; }
    public required string Name { get; init; }
    public required int Weight { get; init; }
    public required string Description { get; init; }
    public required double TargetValue { get; init; } 
}

public sealed record UpdateMetricResponseApiDto
{
    public required MetricApiDto Item { get; init; }
}