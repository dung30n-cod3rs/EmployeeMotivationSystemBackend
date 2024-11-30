namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyMetricsByIdRequestApiDto
{
    public required DateTime CreationDateFrom { get; init; }
    public required DateTime CreationDateTo { get; init; }
    
    public required int CompanyId { get; init; }
    
    public required int FilialId { get; init; }
    public required int PositionId { get; init; }
    public required int MetricId { get; init; }
}

public sealed record GetCompanyMetricsByIdResponseApiDto
{
    
}