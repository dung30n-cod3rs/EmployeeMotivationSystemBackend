using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Companies;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class CompaniesController : BaseController
{
    public CompaniesController(AppDbContext dbContext) 
        : base(dbContext) { }
    
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
    public async Task<int> GetCompanyMembersById(int id)
    {
        throw new NotImplementedException();
    }
    
    // [HttpGet("{id:int}/rating")] filter?
    // public async Task<int> GetCompanyRatingById(int id)
    // {
    //     throw new NotImplementedException();
    // }
    
    // [HttpGet("{id:int}/rating")] filter?
    // public async Task<int> GetCompanyMetricsById(int id)
    // {
    //     throw new NotImplementedException();
    // }
    
    [HttpGet("{id:int}/positions")]
    public async Task<int> GetCompanyPositionsById(int id)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public async Task<CreateCompanyResponseApiDto> CreateCompany([FromBody] CreateCompanyRequestApiDto request)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost("{id:int}/member/{memberId:int}")]
    public async Task<CreateCompanyResponseApiDto> UpdateCompanyMember(int id, int memberId)
    {
        throw new NotImplementedException();
    }
    
    [HttpDelete("{id:int}/member/{memberId:int}")]
    public async Task DeleteCompanyMember(int id, int memberId)
    {
        throw new NotImplementedException();
    }
}