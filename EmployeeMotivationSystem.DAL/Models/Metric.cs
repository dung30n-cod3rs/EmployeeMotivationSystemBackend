using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Metric : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; set; }
    
    public required int PositionId { get; set; }
    public Position Position { get; init; }
    
    public required int Weight { get; set; }
    public required string Description { get; set; }
    public double TargetValue { get; set; }
}