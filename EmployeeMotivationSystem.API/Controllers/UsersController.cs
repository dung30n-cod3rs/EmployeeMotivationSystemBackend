using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Users;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class UsersController : BaseController
{
    public UsersController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    // TODO: Authorize
    [HttpGet("{id:int}")]
    public async Task<GetUserByIdResponseApiDto> GetUserById(int id)
    {
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == id);

        if (user == null)
            throw new Exception($"User with id: {id}");
        
        return new GetUserByIdResponseApiDto
        {
            Item = new UserApiDto
            {
                CreationDate = user.CreationDate,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            }
        };
    }
    
    // TODO: Authorize
    [HttpGet("{id:int}/Metrics")]
    public async Task<GetUserMetricsByIdApiDto> GetUserMetricsById(int id)
    {
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == id);

        if (user == null)
            throw new Exception($"User with id: {id} not found!");

        var companiesUsersMetrics = await DbContext.CompaniesUsersMetrics
            // .Include(el => el.Member)
            // .Include(el => el.Metric)
            .Where(el => el.Member.UserId == user.Id)
            .ToArrayAsync();
        
        return new GetUserMetricsByIdApiDto
        {
            Items = companiesUsersMetrics.Select(el => new GetUserMetricsByIdApiDto.Item
            {
                MetricId = el.MetricId,
                MetricName = el.Metric.Name,
                MetricWeight = el.Metric.Weight,
                Description = el.Metric.Description,
                TargetValue = el.Metric.TargetValue,
                Count = el.Count,
            })
        };
    }
    
    // TODO: Authorize
    [HttpGet("{id:int}/Bonus")]
    public async Task<int> GetUserBonus(int id)
    {
        return 1; // TODO: ?
    }
    
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordRequestApiDto request)
    {
        var isRefreshTokenExists = Request.Cookies.TryGetValue(AuthController.CookieRefreshTokenName, out var outValue);

        if (!isRefreshTokenExists)
            throw new Exception("Bad cookie refresh token!");

        var adminCompanyUserRefreshToken = await DbContext.RefreshTokens
            .Include(el => el.User)
            .FirstOrDefaultAsync(el => el.Token == outValue);

        if (adminCompanyUserRefreshToken == null)
            throw new Exception("Maybe session of company admin is expired!");

        var company = await DbContext.Companies
                .SingleOrDefaultAsync(el => el.CreatorUserId == adminCompanyUserRefreshToken.UserId);

        if (company == null)
            throw new Exception("Maybe you are not a admin of company!");

        if (request.Password != request.RepeatPassword)
            throw new Exception("Password and RepeatPassword fields are not equal!");

        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == request.Id);

        if (user == null)
            throw new Exception($"User with id: {request.Id} not found!");

        user.Password = request.Password;

        await DbContext.SaveChangesAsync();
        
        return Ok();
    }
}