using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record Company : BaseModel
{
    [MaxLength(100)]
    public required string Name { get; init; }
    
    public int CreatorUserId { get; init; }
    public User? CreatorUser { get; init; }

    public List<Filial> Filials { get; init; } = [];
}