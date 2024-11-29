namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUser : BaseModel
{
    public required int CompanyId { get; init; }
    public required Company Company { get; init; }
    
    public required int UserId { get; init; }
    public required User User { get; init; }
    
    public required int PositionId { get; init; }
    public required Position Position { get; init; }
    
    public required int Salary { get; init; }
}