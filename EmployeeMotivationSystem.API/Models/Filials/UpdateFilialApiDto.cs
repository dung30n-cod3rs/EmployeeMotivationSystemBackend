using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Filials;

public sealed record UpdateFilialRequestApiDto
{
    public required int FilialId { get; init; }
    
    public required string Name { get; init; }
    public required string Address { get; init; }
    public required int CompanyId { get; init; }
}

public sealed record UpdateFilialResponseApiDto
{
    public required FilialApiDto Item { get; init; }
}