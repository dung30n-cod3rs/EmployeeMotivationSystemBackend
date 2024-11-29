using System.ComponentModel.DataAnnotations;

namespace EmployeeMotivationSystem.DAL.Models;

public sealed record User : BaseModel
{
    /// <summary>
    /// ФИО
    /// </summary>
    [MaxLength(200)]
    public required string Name { get; init; }
    
    /// <summary>
    /// Email
    /// </summary>
    public required string Email { get; init; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; init; }
}