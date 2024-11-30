using EmployeeMotivationSystem.DAL;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class MemberController : BaseController
{
    public MemberController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    
}