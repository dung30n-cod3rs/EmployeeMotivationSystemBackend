namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUser : BaseModel
{
    public required int CompanyId { get; set; }
    public Company Company { get; init; }
    
    public required int UserId { get; set; }
    public User User { get; init; }
    
    public required int PositionId { get; set; }
    public Position Position { get; init; }
    
    public required double Salary { get; init; }
}