using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Filial : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; init; }
    
    [MaxLength(200)]
    public required string Address { get; init; }
    
    public required int CompanyId { get; init; }
    public required Company Company { get; init; }
}