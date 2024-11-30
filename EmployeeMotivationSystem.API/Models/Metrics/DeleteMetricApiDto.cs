namespace EmployeeMotivationSystem.API.Models.Metrics;

public sealed record DeleteMetricRequestApiDto
{
    public required int Id { get; init; }
}