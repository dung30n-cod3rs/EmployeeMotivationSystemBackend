namespace EmployeeMotivationSystem.API.Models.Base;

public class MetricApiDto
{
    public required int Id { get; init; }
    public required DateTime CreationDate { get; init; }
    public required string Name { get; init; }
    public required int Weight { get; init; }
    public required string Description { get; init; }
    public double TargetValue { get; init; }
    
    public required PositionApiDto Position { get; init; }
}