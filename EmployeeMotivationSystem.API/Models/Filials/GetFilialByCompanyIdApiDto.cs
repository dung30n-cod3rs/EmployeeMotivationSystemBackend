using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Filials;

public sealed record GetFilialByCompanyIdApiDto
{
    public required IEnumerable<FilialApiDto> Items { get; init; } = [];
}