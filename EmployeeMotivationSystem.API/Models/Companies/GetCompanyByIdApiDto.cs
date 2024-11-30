using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Companies;

public class GetCompanyByIdResponseApiDto
{
    public required CompanyApiDto Item { get; init; }
}