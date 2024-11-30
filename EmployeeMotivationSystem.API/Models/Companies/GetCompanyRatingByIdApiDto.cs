namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyRatingByFilterRequestApiDto
{
    public required DateTime CreationDateFrom { get; init; }
    public required DateTime CreationDateTo { get; init; }
    
    public required int CompanyId { get; init; }
    
    public required int FilialId { get; init; }
    public required int PositionId { get; init; }
    public required int MetricId { get; init; }
}

public sealed record GetCompanyRatingByFilterResponseApiDto
{
    public required IEnumerable<GetCompanyRatingByFilterItemResponseApiDto> Items { get; init; } = [];
    
    public sealed record GetCompanyRatingByFilterItemResponseApiDto
    {
        public required string Name { get; init; }
        public required double TargetValue { get; init; }
        public required int MemberValue { get; init; }
    }
}