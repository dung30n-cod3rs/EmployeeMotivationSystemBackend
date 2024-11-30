using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Positions;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
public sealed class PositionsController : BaseController
{
    public PositionsController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost]
    public async Task<AddPositionResponseApiDto> AddPosition([FromBody] AddPositionRequestApiDto request)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");
        
        var newPosition = await DbContext.Positions.AddAsync(new Position
        {
            Name = request.Name,
            Weight = request.Weight,
            CompanyId = request.CompanyId
        });

        await DbContext.SaveChangesAsync();

        return new AddPositionResponseApiDto
        {
            Item = new PositionApiDto
            {
                CreationDate = newPosition.Entity.CreationDate,
                Name = newPosition.Entity.Name,
                Weight = newPosition.Entity.Weight,
                
                Company = new CompanyApiDto
                {
                    CreationDate = company.CreationDate,
                    Name = company.Name
                }
            }
        };
    }
    
    [HttpPut]
    public async Task<UpdatePositionResponseApiDto> UpdatePosition([FromBody] UpdatePositionRequestApiDto request)
    {
        var position = await DbContext.Positions
            .SingleOrDefaultAsync(el => el.Id == request.PositionId);
        
        if (position == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");
        
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");

        position.Name = request.Name;
        position.Weight = request.Weight;
        position.CompanyId = request.CompanyId;

        await DbContext.SaveChangesAsync();

        return new UpdatePositionResponseApiDto
        {
            Item = new PositionApiDto
            {
                CreationDate = position.CreationDate,
                Name = position.Name,
                Weight = position.Weight,
                
                Company = new CompanyApiDto
                {
                    CreationDate = company.CreationDate,
                    Name = company.Name
                }
            }
        };
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeletePosition([FromBody] DeletePositionRequestApiDto request)
    {
        var position = await DbContext.Positions
            .SingleOrDefaultAsync(el => el.Id == request.Id);

        if (position == null)
            throw new Exception($"Position with id: {request.Id} not found!");
        
        DbContext.Positions.Remove(position);

        return Ok();
    }
}