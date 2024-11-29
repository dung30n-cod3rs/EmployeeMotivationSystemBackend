using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

public class CompaniesController : BaseController
{
    public CompaniesController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpGet("{id:int}")]
    public async Task<int> GetCompanyById(int id)
    {
        return 1;
    }

    [HttpGet("{id:int}/members")]
    public async Task<int> GetCompanyMembersById(int id)
    {
        return 1;
    }
    
    // [HttpGet("{id:int}/rating")] filter?
    // public async Task<int> GetCompanyRatingById(int id)
    // {
    //     return 1;
    // }
    
    // [HttpGet("{id:int}/rating")] filter?
    // public async Task<int> GetCompanyMetricsById(int id)
    // {
    //     return 1;
    // }
    
    [HttpGet("{id:int}/positions")]
    public async Task<int> GetCompanyPositionsById(int id)
    {
        return 1;
    }

    // [HttpPost]
    // public async Task<int> CreateCompany(int id)
    // {
    //     return 1;
    // }
    //
    // [HttpPost]
    // public async Task<int> CreateCompany(int id)
    // {
    //     return 1;
    // }

}