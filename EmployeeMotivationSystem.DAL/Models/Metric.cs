using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Metric : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; init; }
    
    public required int PositionId { get; init; }
    public required Position Position { get; init; }
    
    public required int Weight { get; init; }
    public required string Description { get; init; }
    public double TargetValue { get; init; }
}