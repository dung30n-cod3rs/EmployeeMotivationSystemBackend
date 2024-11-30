namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyMetricsByIdRequestApiDto
{
    public required int UserId { get; init; }
    
    public required DateTime CreationDateFrom { get; init; }
    public required DateTime CreationDateTo { get; init; }
}

public sealed record GetCompanyMetricsByIdResponseApiDto
{
    public required IEnumerable<GetCompanyMetricsByIdItemResponseApiDto> Items { get; init; }

    public sealed record GetCompanyMetricsByIdItemResponseApiDto
    {
        public required string Name { get; init; }
        public required int Weight { get; init; }
        public required string Description { get; init; }
        public required double TargetValue { get; init; }
        public required int Count { get; init; }
        public required double Bonus { get; init; }
    }
}