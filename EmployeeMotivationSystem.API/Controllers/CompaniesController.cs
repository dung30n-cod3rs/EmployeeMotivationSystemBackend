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
        var member = await DbContext.CompaniesUsers
            .SingleOrDefaultAsync(el => el.UserId == request.UserId);
        
        if (member == null)
            throw new Exception($"Member with id: {request.UserId} not found or not pinned to any company!");

        var metricIdToMetricCount = await DbContext.CompaniesUsersMetrics
            .Where(el => el.MemberId == member.Id)
            .Where(el => el.CreationDate >= request.CreationDateFrom)
            .Where(el => el.CreationDate <= request.CreationDateTo)
            .GroupBy(el => el.MetricId)
            .Select(el => new
            {
                el.Key,
                Count = el.Count()
            })
            .Join(
                DbContext.Metrics, 
                obj1 => obj1.Key,
                obj2 => obj2.Id,
                (obj1, obj2) => new
                {
                    Id = obj2.Id,
                    CreationDate = obj2.CreationDate,
                    Name = obj2.Name,
                    Weight = obj2.Weight,
                    Description = obj2.Description,
                    TargetValue = obj2.TargetValue,
                    PositionId = obj2.PositionId,
                    Count = obj1.Count
                }                
            )
            .ToArrayAsync();

        var metrics = metricIdToMetricCount.Select(el => new Metric
        {
            Id = el.Id,
            CreationDate = el.CreationDate,
            Name = el.Name,
            PositionId = el.PositionId,
            Weight = el.Weight,
            Description = el.Description,
            TargetValue = el.TargetValue
        });
        
        return new GetCompanyMetricsByIdResponseApiDto()
        {
            Items = metricIdToMetricCount.Select(el => new GetCompanyMetricsByIdResponseApiDto.GetCompanyMetricsByIdItemResponseApiDto
            {
                Name = el.Name,
                Weight = el.Weight,
                Description = el.Description,
                TargetValue = el.TargetValue,
                Count = el.Count,
                Bonus = UsersController.CalculateBonuses(member.Salary, metrics, el.Count)
            })
        };
    }
    
    [HttpPost("AddMetricToMember")]
    public async Task<AddMetricToMemberResponseApiDto> AddMetricToMember([FromBody] AddMetricToMemberRequestApiDto request)
    {
        await DbContext.CompaniesUsersMetrics.AddRangeAsync(request.Items.Select(el => new CompaniesUsersMetric
        {
            MetricId = el.MetricId,
            MemberId = el.MemberId
        }));

        await DbContext.SaveChangesAsync();
        
        return new AddMetricToMemberResponseApiDto
        {
            AffectedRows = request.Items.Count()
        };
    }
    
    [HttpGet("{id:int}/positions")]
    public async Task<GetCompanyPositionByIdResponseApiDto> GetCompanyPositionsById(int id)
    {
        if (HttpContext.Items[JwtMiddleware.JwtTokenHttpContextKey] is not string userEmail)
            throw new Exception("Very strange. Your email is null. I think something its broken.");

        var userFromJwt = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Email == userEmail);

        if (userFromJwt == null)
            throw new Exception("User from jwt is bad, its not good.");

        var userFromJwtCompanies = await DbContext.CompaniesUsers
            .Include(companiesUser => companiesUser.Position)
            .SingleOrDefaultAsync(el => el.UserId == userFromJwt.Id);

        if (userFromJwtCompanies == null)
            throw new Exception($"Member with id = {userFromJwt.Id} not found!");

        var positionWeight = userFromJwtCompanies.Position.Weight;
        
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == id);

        if (company == null)
            throw new Exception($"Company with id: {id} not found!");

        var positions = await DbContext.Positions
            .Where(el => el.CompanyId == company.Id)
            .Where(el => el.Weight <= positionWeight)
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
        
        var newPosition = await DbContext.Positions.AddAsync(new Position
        {
            Name = "Тех. Администратор",
            Weight = 0,
            CompanyId = newCompany.Entity.Id
        });
        
        await DbContext.SaveChangesAsync();
        
        await DbContext.CompaniesUsers.AddAsync(new CompaniesUser
        {
            CompanyId = newCompany.Entity.Id,
            UserId = user.Id,
            PositionId = newPosition.Entity.Id,
            Salary = 0
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