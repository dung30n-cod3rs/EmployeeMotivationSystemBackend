namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUser : BaseModel
{
    public required int CompanyId { get; set; }
    public required Company Company { get; init; }
    
    public required int UserId { get; set; }
    public required User User { get; init; }
    
    public required int PositionId { get; set; }
    public required Position Position { get; init; }
    
    public required int Salary { get; init; }
}