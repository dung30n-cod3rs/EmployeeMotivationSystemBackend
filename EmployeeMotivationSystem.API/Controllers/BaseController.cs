using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

// Maybe we need grpc?

[ApiController]
[Route("api/v1/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected AppDbContext DbContext { get; }

    protected BaseController(AppDbContext dbContext)
    {
        DbContext = dbContext;
    }
}