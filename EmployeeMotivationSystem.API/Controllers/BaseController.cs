using EmployeeMotivationSystem.API.Middleware.Jwt;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[JwtMiddleware]
public abstract class BaseController : ControllerBase
{
    protected AppDbContext DbContext { get; }

    protected BaseController(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
}