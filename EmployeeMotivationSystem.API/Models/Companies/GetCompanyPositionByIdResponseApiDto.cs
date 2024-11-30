using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyPositionByIdResponseApiDto
{
    public required IEnumerable<PositionApiDto> Items = [];
}