using EmployeeMotivationSystem.API.Models.Members;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
public sealed class MembersController : BaseController
{
    public MembersController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost]
    public async Task<IActionResult> AddMember([FromBody] AddMemberRequestApiDto request)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId}");

        var position = await DbContext.Positions
            .SingleOrDefaultAsync(el => el.Id == request.PositionId);

        if (position == null)
            throw new Exception($"Position with id: {request.PositionId}");
        
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Email == request.UserEmail);

        if (user != null)
            throw new Exception($"User with email: {request.UserEmail} already exists!");

        var newUser = await DbContext.Users.AddAsync(new User
        {
            Name = request.UserName,
            Email = request.UserEmail,
            Password = request.UserPassword
        });

        await DbContext.SaveChangesAsync();
        
        await DbContext.CompaniesUsers.AddAsync(new CompaniesUser
        {
            CompanyId = request.CompanyId,
            UserId = newUser.Entity.Id,
            PositionId = request.PositionId,
            Salary = request.Salary
        });
        
        await DbContext.SaveChangesAsync();
        
        return Ok();
    }
}