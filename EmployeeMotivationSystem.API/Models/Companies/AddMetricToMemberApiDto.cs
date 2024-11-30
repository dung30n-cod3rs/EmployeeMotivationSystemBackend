namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record AddMetricToMemberRequestApiDto
{
    public IEnumerable<AddMetricToMemberRequestItemApiDto> Items { get; init; } = [];
    
    public sealed record AddMetricToMemberRequestItemApiDto
    {
        public required int MemberId { get; init; }
        public required int MetricId { get; init; }
    }
}

public sealed record AddMetricToMemberResponseApiDto
{
    public required int AffectedRows { get; init; }
}