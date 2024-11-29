namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUsersFilials : BaseModel
{
    public required int CompanyUserId { get; init; }
    public required CompaniesUser CompanyUser { get; init; }
    
    public required int FilialId { get; init; }
    public required Filial Filial { get; init; }
}