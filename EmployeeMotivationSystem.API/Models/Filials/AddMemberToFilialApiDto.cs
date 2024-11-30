namespace EmployeeMotivationSystem.API.Models.Filials;

public sealed record AddMemberToFilialRequestApiDto
{
    public required int FilialId { get; init; }
    public required int MemberId { get; init; }
}