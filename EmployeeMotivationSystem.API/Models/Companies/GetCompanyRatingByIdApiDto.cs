namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyRatingByIdRequestApiDto
{
    public required int CompanyId { get; init; }
    
    public required int FilialId { get; init; }
    public required int PositionId { get; init; }
    public required int MemberId { get; init; }
}

public sealed record GetCompanyRatingByIdResponseApiDto
{
    
}