using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public abstract record BaseModel
{
    [Key]
    public int Id { get; init; }
    
    public DateTime CreationDate { get; init; } = DateTime.UtcNow;
}