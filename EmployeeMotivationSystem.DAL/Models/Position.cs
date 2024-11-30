using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Position : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; init; }
    public required int Weight { get; init; }
    
    public required int CompanyId { get; init; }
    public required Company Company { get; init; }
    
    // public int? ParentId { get; init; }
    // public Position? Parent { get; init; }
    // public List<Position> Children { get; init; } = [];
}