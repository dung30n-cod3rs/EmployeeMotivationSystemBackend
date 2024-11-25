using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record User : BaseModel
{
    [MaxLength(100)]
    public string? Name { get; init; }
}