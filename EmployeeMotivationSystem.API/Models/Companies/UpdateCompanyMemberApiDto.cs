namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record UpdateCompanyMemberRequestApiDto
{
    public required int CompanyId { get; init; }
    public required int MemberId { get; init; }
    public required int PositionId { get; init; }
    
    public required double Salary { get; init; } 
}