using EmployeeMotivationSystem.API.Models.Metrics;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class MetricsController : BaseController
{
    public MetricsController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost]
    public async Task<AddMetricResponseApiDto> AddMetric([FromBody] AddMetricRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<UpdateMetricResponseApiDto> UpdatePosition([FromBody] UpdateMetricRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public async Task<DeleteMetricResponseApiDto> DeletePosition([FromBody] DeleteMetricRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
}