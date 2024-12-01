using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Filials;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
public sealed class FilialsController : BaseController
{
    public FilialsController(AppDbContext dbContext) 
        : base(dbContext) { }

    [HttpGet("{id:int}")]
    public async Task<GetFilialByIdResponseApiDto> GetFilialById(int id)
    {
        var filial = await DbContext.Filials
            .Include(filial => filial.Company)
            .SingleOrDefaultAsync(el => el.Id == id);

        if (filial == null)
            throw new Exception($"Filial with id: {id} not found!");

        return new GetFilialByIdResponseApiDto
        {
            Item = new FilialApiDto
            {
                Id = filial.Id,
                CreationDate = filial.CreationDate,
                Name = filial.Name,
                Address = filial.Address,
                Company = new CompanyApiDto
                {
                    Id = filial.Id,
                    CreationDate = filial.Company.CreationDate,
                    Name = filial.Company.Name
                }
            }
        };
    }

    [HttpGet("ByCompany")]
    public async Task<GetFilialByCompanyIdApiDto> GetFilialsByCompanyId(int companyId)
    {
        var filials = await DbContext.Filials
            .Include(el => el.Company)
            .Where(el => el.CompanyId == companyId)
            .ToArrayAsync();

        return new GetFilialByCompanyIdApiDto
        {
            Items = filials.Select(el => new FilialApiDto
            {
                Id = el.Id,
                CreationDate = el.CreationDate,
                Name = el.Name,
                Address = el.Address,
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
    public async Task<CreateFilialResponseApiDto> CreateFilial([FromBody] CreateFilialRequestApiDto request)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");

        var newFilial = await DbContext.Filials.AddAsync(new Filial
        {
            Name = request.Name,
            Address = request.Address,
            CompanyId = company.Id
        });

        await DbContext.SaveChangesAsync();

        return new CreateFilialResponseApiDto
        {
            Item = new FilialApiDto
            {
                Id = newFilial.Entity.Id,
                CreationDate = newFilial.Entity.CreationDate,
                Name = newFilial.Entity.Name,
                Address = newFilial.Entity.Address,
                Company = new CompanyApiDto
                {
                    Id = company.Id,
                    CreationDate = company.CreationDate,
                    Name = company.Name
                }
            }
        };
    }

    [HttpPost("AddMemberToFilial")]
    public async Task<IActionResult> AddMemberToFilial([FromBody] AddMemberToFilialRequestApiDto request)
    {
        var filial = await DbContext.Filials
            .SingleOrDefaultAsync(el => el.Id == request.FilialId);

        if (filial == null)
            throw new Exception($"Filial with id: {request.FilialId} not found!");
        
        var member = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == request.MemberId);

        if (member == null)
            throw new Exception($"Member with id: {request.FilialId} not found!");

        var companyUser = await DbContext.CompaniesUsers
            .SingleOrDefaultAsync(el => el.UserId == request.MemberId);

        if (companyUser == null)
            throw new Exception($"CompanyUser with UserId: {request.MemberId} not found, i think something its broken.");
        
        await DbContext.CompaniesUsersFilials
            .AddAsync(new CompaniesUsersFilials
            {
                CompanyUserId = companyUser.Id,
                FilialId = request.FilialId,
            });

        await DbContext.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPut]
    public async Task<UpdateFilialResponseApiDto> UpdateFilial([FromBody] UpdateFilialRequestApiDto request)
    {
        var filial = await DbContext.Filials
            .SingleOrDefaultAsync(el => el.Id == request.FilialId);

        if (filial == null)
            throw new Exception($"Filial with id: {request.CompanyId} not found!");
        
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");

        filial.Name = request.Name;
        filial.Address = request.Address;
        filial.CompanyId = company.Id;
        
        await DbContext.SaveChangesAsync();

        return new UpdateFilialResponseApiDto
        {
            Item = new FilialApiDto
            {
                Id = filial.Id,
                CreationDate = filial.CreationDate,
                Name = filial.Name,
                Address = filial.Address,
                Company = new CompanyApiDto
                {
                    Id = company.Id,
                    CreationDate = company.CreationDate,
                    Name = company.Name
                }
            }
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFilial(int id)
    {
        var filial = await DbContext.Filials
            .Include(filial => filial.Company)
            .SingleOrDefaultAsync(el => el.Id == id);

        if (filial == null)
            throw new Exception($"Filial with id: {id} not found!");

        DbContext.Filials.Remove(filial);
        
        await DbContext.SaveChangesAsync();
        
        return Ok();
    }
}