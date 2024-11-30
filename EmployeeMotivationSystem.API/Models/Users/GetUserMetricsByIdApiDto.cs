namespace EmployeeMotivationSystem.API.Models.Users;

public sealed record GetUserMetricsByIdApiDto
{
    public IEnumerable<GetUserMetricsByIdItemApiDto> Items { get; init; } = [];

    public record GetUserMetricsByIdItemApiDto
    {
        public required int MetricId { get; init; }
        public required string MetricName { get; init; }
        public required int MetricWeight { get; init; }
        public required string Description { get; init; }
        public required double TargetValue { get; init; }
        
        public required int Count { get; init; }
    }
}
