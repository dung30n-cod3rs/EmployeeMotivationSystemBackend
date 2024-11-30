using EmployeeMotivationSystem.API.Models.Members;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class MembersController : BaseController
{
    public MembersController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost]
    public async Task<AddMemberResponseApiDto> AddMember([FromBody] AddMemberRequestApiDto request) 
    {
        throw new NotImplementedException();
    }
}