using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyMetricsByIdRequestApiDto
{
    public required int CompanyId { get; init; }
    public required int PositionId { get; init; }
}

public sealed record GetCompanyMetricsByIdResponseApiDto
{
    public required IEnumerable<MetricApiDto> Items { get; init; }
}