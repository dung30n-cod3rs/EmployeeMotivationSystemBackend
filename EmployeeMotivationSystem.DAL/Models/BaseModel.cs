namespace EmployeeMotivationSystem.DAL.Models;

public abstract record BaseModel
{
    public Guid Id { get; init; }
    public DateTime CreationDate { get; init; }
}