namespace EmployeeMotivationSystem.API.Models.Users;

public sealed record GetUserMetricsByIdRequestApiDto
{
    public int UserId { get; init; }
    public DateTime DateFrom { get; init; }
    public DateTime DateTo { get; init; }
}

public sealed record GetUserMetricsByIdResponseApiDto
{
    public IEnumerable<GetUserMetricsByIdResponseItemApiDto> Items { get; init; } = [];

    public record GetUserMetricsByIdResponseItemApiDto
    {
        public required int MetricId { get; init; }
        public required string MetricName { get; init; }
        public required int MetricWeight { get; init; }
        public required string MetricDescription { get; init; }
        public required double MetricTargetValue { get; init; }
        
        public required int Count { get; init; }
        public required double Bonuses { get; init; }
    }
}
