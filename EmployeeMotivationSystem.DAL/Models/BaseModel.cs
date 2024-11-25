namespace EmployeeMotivationSystem.DAL.Models;

public abstract record BaseModel
{
    public Guid Id { get; init; }
}