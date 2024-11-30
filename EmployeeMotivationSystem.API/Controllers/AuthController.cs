using System.Security.Claims;
using EmployeeMotivationSystem.API.Constants;
using EmployeeMotivationSystem.API.Models.Auth;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

public sealed class AuthController : BaseController
{
    public const string CookieRefreshTokenName = "RefreshToken";
    
    public AuthController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpPost("Register")]
    public async Task<RegisterUserResponseApiDto> Register([FromBody] RegisterUserRequestApiDto request)
    {
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Email == request.Email);

        if (user != null)
            throw new Exception($"User with email: {request.Email} already exists!");

        if (request.Password != request.RepeatPassword)
            throw new Exception("Password and RepeatPassword fields are not equal!");
        
        var createdUser = await DbContext.Users.AddAsync(new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = request.Password
        });

        await DbContext.SaveChangesAsync();

        var (jwtToken, refreshToken) = await GenerateAndSaveJwtAndRefreshToken(createdUser.Entity);
        
        Response.Cookies.Append(CookieRefreshTokenName, refreshToken);
        
        return new RegisterUserResponseApiDto
        {
            Token = jwtToken,
            RefreshToken = refreshToken
        };
    }
    
    [HttpPost("Login")]
    public async Task<LoginUserResponseApiDto> Login([FromBody] LoginUserRequestApiDto request)
    {
        var user = await DbContext.Users.SingleOrDefaultAsync(el =>
            el.Email == request.Email 
            && el.Password == request.Password
        );

        if (user == null)
            throw new Exception("User with requested credentials not found!");
        
        var (jwtToken, refreshToken) = await GenerateAndSaveJwtAndRefreshToken(user);
        
        Response.Cookies.Append(CookieRefreshTokenName, refreshToken);
        
        return new LoginUserResponseApiDto
        {
            Token = jwtToken,
            RefreshToken = refreshToken
        };
    }
    
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        var isRefreshTokenExists = Request.Cookies.TryGetValue(CookieRefreshTokenName, out var outValue);

        if (isRefreshTokenExists)
        {
            var refreshTokenObj = await DbContext.RefreshTokens
                .SingleOrDefaultAsync(el => el.Token == outValue);

            if (refreshTokenObj != null)
                DbContext.RefreshTokens.Remove(refreshTokenObj);
        }
        
        Response.Cookies.Delete(CookieRefreshTokenName);

        return Ok();
    }
    
    [HttpPost("Refresh")]
    public async Task<RefreshUserResponseApiDto> Refresh()
    {
        var isRefreshTokenExists = Request.Cookies.TryGetValue(CookieRefreshTokenName, out var outValue);

        if (isRefreshTokenExists)
        {
            var refreshTokenObj = await DbContext.RefreshTokens
                .Include(refreshToken => refreshToken.User)
                .SingleOrDefaultAsync(el => el.Token == outValue);

            if (refreshTokenObj != null)
            {
                var (jwtToken, refreshToken) = await GenerateAndSaveJwtAndRefreshToken(refreshTokenObj.User);

                return new RefreshUserResponseApiDto
                {
                    Token = jwtToken,
                    RefreshToken = refreshToken
                };
            }
        }

        throw new Exception("Cookie token is bad.");
    }

    private async Task<(string jwt, string refresh)> GenerateAndSaveJwtAndRefreshToken(User user)
    {
        var jwtToken = AppAuthOptions.GenerateJwtToken(new []
        {
            new Claim(AppCustomClaimTypes.UserId, user.Id.ToString()), 
            new Claim(ClaimTypes.Email, user.Email)
        });
        
        var refreshToken = AppAuthOptions.GenerateRefreshToken();

        await DbContext.RefreshTokens.AddAsync(new RefreshToken
        {
            User = user,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(30)
        });
        
        await DbContext.SaveChangesAsync();

        return (jwt: jwtToken, refresh: refreshToken);
    }
}