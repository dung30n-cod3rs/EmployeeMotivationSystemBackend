using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record CreateCompanyRequestApiDto
{
    public required string Name { get; init; }
}

public sealed record CreateCompanyResponseApiDto
{
    public required CompanyApiDto Item { get; init; }
}