using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record User : BaseModel
{
    /// <summary>
    /// Имя
    /// </summary>
    [MaxLength(100)]
    public required string FirstName { get; init; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    [MaxLength(100)]
    public required string LastName { get; init; }
    
    /// <summary>
    /// Отчество
    /// </summary>
    [MaxLength(100)]
    public string? MiddleName { get; init; }
    
    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; init; }
}