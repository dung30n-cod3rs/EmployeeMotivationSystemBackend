using EmployeeMotivationSystem.API.Models.Base;

namespace EmployeeMotivationSystem.API.Models.Users;

public sealed record GetUserByIdResponseApiDto
{
    public required UserApiDto Item { get; init; }
}