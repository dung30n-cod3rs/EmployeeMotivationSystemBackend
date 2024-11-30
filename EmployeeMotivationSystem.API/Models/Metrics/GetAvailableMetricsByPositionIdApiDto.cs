using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Metrics;

public sealed record GetAvailableMetricsByPositionResponseIdApiDto
{
    public IEnumerable<MetricApiDto> Items { get; init; } = [];
}