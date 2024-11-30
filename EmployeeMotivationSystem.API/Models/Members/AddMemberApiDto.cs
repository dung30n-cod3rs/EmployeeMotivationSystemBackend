namespace EmployeeMotivationSystem.API.Models.Members;

public sealed record AddMemberRequestApiDto
{
    public required int CompanyId { get; init; }
    public required int PositionId { get; init; }
    
    public required string UserName { get; init; }
    public required string UserEmail { get; init; }
    public required string UserPassword { get; init; }
    
    public required double Salary { get; init; }
}