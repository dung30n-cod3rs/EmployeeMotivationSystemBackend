using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Position : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }
    public required int Weight { get; set; }
    
    public required int CompanyId { get; set; }
    public Company Company { get; init; }
    
    // public int? ParentId { get; init; }
    // public Position? Parent { get; init; }
    // public List<Position> Children { get; init; } = [];
}