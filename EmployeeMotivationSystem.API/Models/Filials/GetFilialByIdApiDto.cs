using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Filials;

public sealed record GetFilialByIdResponseApiDto
{
    public required FilialApiDto Item { get; init; }
}