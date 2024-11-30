namespace EmployeeMotivationSystem.API.Models.Base;

public sealed record FilialApiDto
{
    public required DateTime CreationDate { get; init; }
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required CompanyApiDto Company { get; init; }
}