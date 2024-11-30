namespace EmployeeMotivationSystem.DAL.Models;

public sealed record CompaniesUsersMetric : BaseModel
{
    public required int MetricId { get; init; }
    public Metric Metric { get; init; }
    
    public required int MemberId { get; init; }
    public CompaniesUser Member { get; init; }
}