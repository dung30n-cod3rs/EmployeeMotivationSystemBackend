namespace EmployeeMotivationSystem.API.Models.Users;

public sealed record GetUserMetricsByIdApiDto
{
    public IEnumerable<Item> Items { get; init; } = [];

    public record Item
    {
        public required int MetricId { get; init; }
        public required string MetricName { get; init; }
        public required int MetricWeight { get; init; }
        public required string Description { get; init; }
        public required double TargetValue { get; init; }
        
        public required int Count { get; init; }
    }
}
