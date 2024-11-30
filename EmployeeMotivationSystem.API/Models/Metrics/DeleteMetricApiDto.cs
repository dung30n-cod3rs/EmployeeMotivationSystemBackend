namespace EmployeeMotivationSystem.API.Models.Metrics;

public sealed record DeleteMetricRequestApiDto
{
    public required int MetricId { get; init; }
}