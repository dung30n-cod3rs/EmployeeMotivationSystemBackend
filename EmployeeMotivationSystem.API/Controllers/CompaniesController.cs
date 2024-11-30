using EmployeeMotivationSystem.API.Middleware.Jwt;
using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Companies;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
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
                Id = company.Id,
                CreationDate = company.CreationDate,
                Name = company.Name
            }
        };
    }

    public class TestObj
    {
        public int Cock { get; set; }
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
    
    [HttpPost("RatingByFilter")]
    public async Task<GetCompanyRatingByFilterResponseApiDto> GetCompanyRatingById([FromBody] GetCompanyRatingByFilterRequestApiDto request)
    {
        var metric = await DbContext.Metrics
            .SingleOrDefaultAsync(el => el.Id == request.MetricId);

        if (metric == null)
            throw new Exception($"Metric with id: {request.MetricId} not found!");

        var filial = await DbContext.Filials
            .Include(filial => filial.Company)
            .SingleOrDefaultAsync(el => el.Id == request.FilialId);

        if (filial == null)
            throw new Exception($"Filial with id: {request.FilialId} not found!");
        
        var position = await DbContext.Filials
            .SingleOrDefaultAsync(el => el.Id == request.PositionId);

        if (position == null)
            throw new Exception($"Filial with id: {request.PositionId} not found!");

        var membersOfCurrentFilial = await DbContext.CompaniesUsersFilials
            .Include(el => el.CompanyUser)
            .ThenInclude(el => el.User)
            .Where(el => el.FilialId == filial.Id)
            .Select(el => el.CompanyUser.User.Id)
            .ToArrayAsync();

        var values = await DbContext.CompaniesUsersMetrics
            .Include(el => el.Metric)
            .Include(el => el.Member)
            .ThenInclude(el => el.User)
            .Where(el => el.Metric.CreationDate >= request.CreationDateFrom)
            .Where(el => el.Metric.CreationDate <= request.CreationDateTo)
            .Where(el => membersOfCurrentFilial.Contains(el.MemberId))
            .Select(el => new
            {
                Name = el.Member.User.Name,
                TargetValue = el.Metric.TargetValue,
                MemberValue = membersOfCurrentFilial.Count(k => k == el.MemberId)
            })
            .OrderBy(el => el.MemberValue)
            .ToArrayAsync();
        
        return new GetCompanyRatingByFilterResponseApiDto
        {
            Items = values.Select(el => new GetCompanyRatingByFilterResponseApiDto.GetCompanyRatingByFilterItemResponseApiDto
            {
                Name = el.Name,
                TargetValue = el.TargetValue,
                MemberValue = el.MemberValue
            })
        };
    }
    
    [HttpPost("MetricsByFilter")]
    public async Task<GetCompanyMetricsByIdResponseApiDto> GetCompanyMetricsById([FromBody] GetCompanyMetricsByIdRequestApiDto request)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");
        
        var position = await DbContext.Filials
            .SingleOrDefaultAsync(el => el.Id == request.PositionId);

        if (position == null)
            throw new Exception($"Filial with id: {request.PositionId} not found!");

        var currentCompanyPositionsIds = await DbContext.Positions
            .Include(el => el.Company)
            .Where(el => el.CompanyId == request.CompanyId)
            .Select(el => el.Id)
            .ToArrayAsync();

        var currentCompanyMetricsByPositions = await DbContext.Metrics
            .Where(el => currentCompanyPositionsIds.Contains(el.Id))
            .ToArrayAsync();

        return new GetCompanyMetricsByIdResponseApiDto()
        {
            Items = currentCompanyMetricsByPositions.Select(el => new MetricApiDto
            {
                Id = el.Id,
                CreationDate = el.CreationDate,
                Name = el.Name,
                Weight = el.Weight,
                Description = el.Description,
                TargetValue = el.TargetValue,
                Position = new PositionApiDto
                {
                    Id = el.Position.Id,
                    CreationDate = el.Position.CreationDate,
                    Name = el.Position.Name,
                    Weight = el.Position.Weight,
                    Company = new CompanyApiDto
                    {
                        Id = el.Position.Company.Id,
                        CreationDate = el.Position.Company.CreationDate,
                        Name = el.Position.Company.Name
                    }
                }
            })
        };
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
                Id = el.Id,
                CreationDate = el.CreationDate,
                Name = el.Name,
                Weight = el.Weight,
                Company = new CompanyApiDto
                {
                    Id = el.Company.Id,
                    CreationDate = el.Company.CreationDate,
                    Name = el.Company.Name
                }
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
                Id = newCompany.Entity.Id,
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