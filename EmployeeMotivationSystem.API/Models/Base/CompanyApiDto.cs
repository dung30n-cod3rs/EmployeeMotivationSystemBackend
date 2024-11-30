namespace EmployeeMotivationSystem.API.Models.Base;

public class CompanyApiDto
{
    public required int Id { get; init; }
    public required DateTime CreationDate { get; init; }
    public required string Name { get; init; }
}