namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUsersFilials : BaseModel
{
    public required int CompanyUserId { get; init; }
    public  CompaniesUser CompanyUser { get; init; }
    
    public required int FilialId { get; init; }
    public  Filial Filial { get; init; }
}