using EmployeeMotivationSystem.API.Middleware.Jwt;
using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Companies;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class CompaniesController : BaseController
{
    public CompaniesController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [Authorize]
    [HttpGet("{id:int}")]
    public async Task<GetCompanyByIdResponseApiDto> GetCompanyById(int id)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == id);

        if (company == null)
            throw new Exception($"Company with id: {id} not found!");
        
        return new GetCompanyByIdResponseApiDto
        {
            Item = new CompanyApiDto
            {
                CreationDate = company.CreationDate,
                Name = company.Name
            }
        };
    }

    [HttpGet("{id:int}/members")]
    public async Task<GetCompanyMembersByIdResponseApiDto> GetCompanyMembersById(int id)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == id);

        if (company == null)
            throw new Exception($"Company with id: {id} not found!");

        var members = await DbContext.CompaniesUsers
            .Include(el => el.Company)
            .Include(el => el.User)
            .Include(el => el.Position)
            .Where(el => el.CompanyId == id)
            .ToArrayAsync();

        return new GetCompanyMembersByIdResponseApiDto()
        {
            Items = members.Select(el => new GetCompanyMembersByIdResponseApiDto.GetCompanyMembersByIdItemResponseApiDto
            {
                CompanyCreationDate = el.Company.CreationDate,
                CompanyName = el.Company.Name,
                UserCreationDate = el.User.CreationDate,
                UserName = el.User.Name,
                UserEmail = el.User.Email,
                PositionCreationDate = el.Position.CreationDate,
                PositionName = el.Position.Name,
                PositionWeight = el.Position.Weight,
                Salary = el.Salary
            })
        };
    }
    
    [HttpGet("RatingByFilter")]
    public async Task GetCompanyRatingById([FromBody] GetCompanyMetricsByIdRequestApiDto request)
    {
        var metric = await DbContext.Metrics
            .SingleOrDefaultAsync(el => el.Id == request.MetricId);

        if (metric == null)
            throw new Exception($"Metric with id: {request.MetricId} not found!");

        var filial = await DbContext.Filials
            .SingleOrDefaultAsync(el => el.Id == request.FilialId);

        if (filial == null)
            throw new Exception($"Filial with id: {request.FilialId} not found!");

        // var a = Com
        
        // var companiesUsersFilials = await DbContext.CompaniesUsersFilials
        //     .Where(el => el.FilialId == request.FilialId)
        //     .ToArrayAsync();
        //
        // var filialUsersIds = companiesUsersFilials
        //     .Select(el => el.CompanyUser)
        //     .ToArray();
        //
        // var a = await DbContext.CompaniesUsersMetrics
        //     .Where(el => el.MetricId == request.MetricId)
        //     .GroupBy(el => el.Member.Id)
        //     .Select(g => new
        //     {
        //         g.Key,
        //         Count = g.Count()
        //     })
        //     .ToArrayAsync();
        //
        // var b = a
        //     .Join(
        //         filialUsersIds, 
        //         u => u.Key, 
        //         g => g.UserId,
        //         (u, g) => new
        //         {
        //             UserId = u.Key,
        //             UserName = g.User.Name,
        //             MetricsCount = u.Count
        //         }
        //     )
        //     .ToArray();
        
        // TODO
        
        throw new NotImplementedException();
    }
    
    // Место, Имя, TargetValue, значение сотрудника
    
    [HttpGet("MetricsByFilter")]
    public async Task<int> GetCompanyMetricsById([FromBody] GetCompanyRatingByIdRequestApiDto request)
    {
        // TODO
        
        throw new NotImplementedException();
    }
    
    [HttpGet("{id:int}/positions")]
    public async Task<GetCompanyPositionByIdResponseApiDto> GetCompanyPositionsById(int id)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == id);

        if (company == null)
            throw new Exception($"Company with id: {id} not found!");

        var positions = await DbContext.Positions
            .Where(el => el.CompanyId == company.Id)
            .ToArrayAsync();

        return new GetCompanyPositionByIdResponseApiDto
        {
            Items = positions.Select(el => new PositionApiDto
            {
                PositionCreationDate = el.CreationDate,
                PositionName = el.Name,
                PositionWeight = el.Weight,
                CompanyCreationDate = el.Company.CreationDate,
                CompanyName = el.Company.Name
            })
        };
    }
    
    [HttpPost]
    public async Task<CreateCompanyResponseApiDto> CreateCompany([FromBody] CreateCompanyRequestApiDto request)
    {
        if (HttpContext.Items[JwtMiddleware.JwtTokenHttpContextKey] is not string userEmail)
            throw new Exception("Very strange. Your email is null. I think something its broken.");

        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Email == userEmail);

        if (user == null)
            throw new Exception("Very strange. You is null. I think something its broken.");

        var userCompany = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.CreatorUserId == user.Id);

        if (userCompany != null)
            throw new Exception($"User already has company with name: {userCompany.Name}");

        var newCompany = await DbContext.Companies.AddAsync(new Company
        {
            Name = request.Name,
            CreatorUserId = user.Id
        });

        await DbContext.SaveChangesAsync();

        return new CreateCompanyResponseApiDto
        {
            Item = new CompanyApiDto
            {
                CreationDate = newCompany.Entity.CreationDate,
                Name = newCompany.Entity.Name
            }
        };
    }
    
    [HttpPut("UpdateCompanyMember")]
    public async Task<IActionResult> UpdateCompanyMember([FromForm] UpdateCompanyMemberRequestApiDto request)
    {
        var companiesUser = await DbContext.CompaniesUsers
            .SingleOrDefaultAsync(el => 
                el.CompanyId == request.CompanyId
                && el.PositionId == request.PositionId
                && el.UserId == request.MemberId
            );
        
        if (companiesUser == null)
            throw new Exception($"" +
                                $"CompanyUser by " +
                                $"companyId: {request.CompanyId}, " +
                                $"positionId: {request.PositionId} " +
                                $"memberId: {request.MemberId}" +
                                $"not found!");

        companiesUser.CompanyId = request.CompanyId;
        companiesUser.UserId = request.MemberId;
        companiesUser.PositionId = request.PositionId;

        await DbContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpDelete("{companyId:int}/member/{memberId:int}")]
    public async Task DeleteCompanyMember(int companyId, int memberId)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == companyId);

        if (company == null)
            throw new Exception($"Company with id: {companyId} not found!");

        // Is that cascade?
        
        // var companyMember = await DbContext.CompaniesUsers
        //     .SingleOrDefaultAsync(el =>
        //         el.CompanyId == companyId
        //         && el.UserId == memberId
        //     );

        // if (companyMember == null)
        //     throw new Exception($"Member with id: {memberId} not found!");
        
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == memberId);

        if (user == null)
            throw new Exception($"Member with id: {memberId} not found!");

        DbContext.Users.Remove(user);

        await DbContext.SaveChangesAsync();
    }
}