using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Metrics;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
public sealed class MetricsController : BaseController
{
    public MetricsController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost]
    public async Task<AddMetricResponseApiDto> AddMetric([FromBody] AddMetricRequestApiDto request)
    {
        var position = await DbContext.Positions
            .Include(position => position.Company)
            .SingleOrDefaultAsync(el => el.Id == request.PositionId);

        if (position == null)
            throw new Exception($"Position with id: {request.PositionId} not found!");

        var newMetric = await DbContext.Metrics.AddAsync(new Metric
        {
            Name = request.Name,
            PositionId = request.PositionId,
            Weight = request.Weight,
            Description = request.Description,
            TargetValue = request.TargetValue
        });
        
        await DbContext.SaveChangesAsync();

        return new AddMetricResponseApiDto
        {
            Item = new MetricApiDto
            {
                Id = newMetric.Entity.Id,
                CreationDate = newMetric.Entity.CreationDate,
                Name = newMetric.Entity.Name,
                Weight = newMetric.Entity.Weight,
                Description = newMetric.Entity.Description,
                TargetValue = newMetric.Entity.TargetValue,
                Position = new PositionApiDto
                {
                    Id = position.Id,
                    CreationDate = position.CreationDate,
                    Name = position.Name,
                    Weight = position.Weight,
                    Company = new CompanyApiDto
                    {
                        Id = position.Company.Id,
                        CreationDate = position.Company.CreationDate,
                        Name = position.Company.Name
                    }
                }
            }
        };
    }
    
    [HttpPut]
    public async Task<UpdateMetricResponseApiDto> UpdatePosition([FromBody] UpdateMetricRequestApiDto request) 
    {
        var metric = await DbContext.Metrics
            .SingleOrDefaultAsync(el => el.Id == request.MetricId);

        if (metric == null)
            throw new Exception($"Metric with id: {request.PositionId} not found!");
        
        var position = await DbContext.Positions
            .Include(position => position.Company)
            .SingleOrDefaultAsync(el => el.Id == request.PositionId);

        if (position == null)
            throw new Exception($"Position with id: {request.PositionId} not found!");

        metric.PositionId = request.PositionId;
        metric.Name = request.Name;
        metric.Weight = request.Weight;
        metric.Description = request.Description;
        metric.TargetValue = request.TargetValue;

        await DbContext.SaveChangesAsync();

        return new UpdateMetricResponseApiDto
        {
            Item = new MetricApiDto
            {
                Id = metric.Id,
                CreationDate = metric.CreationDate,
                Name = metric.Name,
                Weight = metric.Weight,
                Description = metric.Description,
                TargetValue = metric.TargetValue,
                Position = new PositionApiDto
                {
                    Id = position.Id,
                    CreationDate = position.CreationDate,
                    Name = position.Name,
                    Weight = position.Weight,
                    Company = new CompanyApiDto
                    {
                        Id = position.Company.Id,
                        CreationDate = position.Company.CreationDate,
                        Name = position.Company.Name
                    }
                }
            }
        };
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePosition([FromBody] DeleteMetricRequestApiDto request)
    {
        var metric = await DbContext.Metrics
            .SingleOrDefaultAsync(el => el.Id == request.Id);

        if (metric == null)
            throw new Exception($"Metric with id: {request.Id} not found!");

        DbContext.Metrics.Remove(metric);
        
        await DbContext.SaveChangesAsync();
        
        return Ok();
    }
}