using EmployeeMotivationSystem.API.Models.Positions;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class PositionsController : BaseController
{
    public PositionsController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost]
    public async Task<AddPositionResponseApiDto> AddPosition([FromBody] AddPositionRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
    
    [HttpPut]
    public async Task<UpdatePositionResponseApiDto> UpdatePosition([FromBody] UpdatePositionRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete]
    public async Task<DeletePositionResponseApiDto> DeletePosition([FromBody] DeletePositionRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
}