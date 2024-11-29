using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class UsersController : BaseController
{
    public UsersController(AppDbContext dbContext) : base(dbContext)
    {
    }
    
    [HttpGet("{id:int}")]
    public async Task<int> GetUserById(int id)
    {
        return 1;
    }
    
    [HttpGet("{id:int}/metrics")]
    public async Task<int> GetUserMetricsById(int id)
    {
        return 1;
    }
    
    [HttpGet("{id:int}/bonus")]
    public async Task<int> GetUserBonus(int id)
    {
        return 1;
    }
    
    [HttpGet("{id:int}/password")]
    public async Task<int> ChangePassword(int id)
    {
        return 1;
    }
    
    // [Authorize]
    // [HttpGet("Get1")]
    // public async Task<int> Get1()
    // {
    //     return 2;
    // }
    
    // [HttpGet("Get1/{userName}")]
    // public async Task<string> Get1(string userName)
    // {
    //     var claims = new List<Claim> {new Claim(ClaimTypes.Name, userName) };
    //     
    //     var jwt = new JwtSecurityToken(
    //         issuer: AppAuthOptions.Issuer,
    //         audience: AppAuthOptions.Audience,
    //         claims: claims,
    //         expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(100)), // время действия 100 минуты
    //         signingCredentials: new SigningCredentials(AppAuthOptions.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));
    //         
    //     return new JwtSecurityTokenHandler().WriteToken(jwt);
    // }
    
    
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> Get()
    // {
    //     return await db.Users.ToListAsync();
    // }
    //
    // // GET api/users/5
    // [HttpGet("{id}")]
    // public async Task<ActionResult<User>> Get(int id)
    // {
    //     User user = await db.Users.FirstOrDefaultAsync(x => x.Id == id);
    //     if (user == null)
    //         return NotFound();
    //     return new ObjectResult(user);
    // }
    //
    // // POST api/users
    // [HttpPost]
    // public async Task<ActionResult<User>> Post(User user)
    // {
    //     if (user == null)
    //     {
    //         return BadRequest();
    //     }
    //
    //     db.Users.Add(user);
    //     await db.SaveChangesAsync();
    //     return Ok(user);
    // }
    //
    // // PUT api/users/
    // [HttpPut]
    // public async Task<ActionResult<User>> Put(User user)
    // {
    //     if (user == null)
    //     {
    //         return BadRequest();
    //     }
    //     if (!db.Users.Any(x => x.Id ==user.Id))
    //     {
    //         return NotFound();
    //     }
    //
    //     db.Update(user);
    //     await db.SaveChangesAsync();
    //     return Ok(user);
    // }
    //
    // // DELETE api/users/5
    // [HttpDelete("{id}")]
    // public async Task<ActionResult<User>> Delete(int id)
    // {
    //     User user = db.Users.FirstOrDefault(x => x.Id == id);
    //     if (user == null)
    //     {
    //         return NotFound();
    //     }
    //     db.Users.Remove(user);
    //     await db.SaveChangesAsync();
    //     return Ok(user);
    // }
    
}