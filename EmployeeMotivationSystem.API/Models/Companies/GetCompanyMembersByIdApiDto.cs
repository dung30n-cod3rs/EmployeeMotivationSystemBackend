namespace EmployeeMotivationSystem.API.Models.Companies;

public sealed record GetCompanyMembersByIdResponseApiDto
{
    public IEnumerable<GetCompanyMembersByIdItemResponseApiDto> Items = [];

    public sealed record GetCompanyMembersByIdItemResponseApiDto
    {
        public required DateTime CompanyCreationDate { get; init; }
        public required string CompanyName { get; init; }
        
        public required DateTime UserCreationDate { get; init; }
        public required string UserName { get; init; }
        public required string UserEmail { get; init; }
        
        public required DateTime PositionCreationDate { get; init; }
        public required string PositionName { get; init; }
        public required int PositionWeight { get; init; }
        
        public required int Salary { get; init; }
    }
}