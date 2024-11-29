using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

public class AuthController : BaseController
{
    public AuthController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost("login")]
    public async Task<int> Login()
    {
        return 1;
    }
    
    [HttpPost("register")]
    public async Task<int> Register()
    {
        return 1;
    }
    
    [HttpPost("logout")]
    public async Task<int> Logout()
    {
        return 1;
    }
    
    [HttpPost("refresh")]
    public async Task<int> Refresh()
    {
        return 1;
    }
}