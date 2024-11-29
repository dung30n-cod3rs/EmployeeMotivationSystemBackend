namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUsersMetric : BaseModel
{
    public required int MetricId { get; init; }
    public required Metric Metric { get; init; }
    
    public required int MemberId { get; init; }
    public required CompaniesUser Member { get; init; }
    
    public required int Count { get; init; }
}