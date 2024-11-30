using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Filial : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MaxLength(200)]
    public required string Address { get; set; }
    
    public required int CompanyId { get; set; }
    public Company Company { get; init; }
}