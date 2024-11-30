using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmployeeMotivationSystem.API.Constants;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeMotivationSystem.API.Middleware.Auth;

public sealed class AuthenticationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // string? authToken = context.HttpContext.Request.Headers.Authorization;
        //
        // authToken = authToken?.Replace("Bearer ", string.Empty);
        //
        // if (string.IsNullOrEmpty(authToken))
        // {
        //     SetRequestResult(context, "Authentication error.", 401);
        //     return;
        // }
        //
        // var handler = new JwtSecurityTokenHandler();
        // var validations = new AppTokenValidationParameters();
        //
        // handler.ValidateToken(authToken, validations, out var validatedToken);
        //
        // var jwtToken = (JwtSecurityToken) validatedToken;
        //
        // var email = jwtToken.Claims.SingleOrDefault(el => el.Type == ClaimTypes.Email)?.Value;
        //
        // if (string.IsNullOrEmpty(email))
        // {
        //     SetRequestResult(context, "Authentication error.", 401);
        //     return;
        // }
        //
        // var dbContext = context.HttpContext
        //     .RequestServices
        //     .GetService(typeof(AppDbContext)) as AppDbContext;
        //
        // if (dbContext == null)
        // {
        //     SetRequestResult(context, "Authentication error.", 401);
        //     return;
        // }
        //
        // var requestUser = await dbContext.Users
        //     .SingleOrDefaultAsync(el => el.Email == email);
        //
        // if (requestUser == null)
        // {
        //     SetRequestResult(context, "Authentication error.", 401);
        //     return;
        // }
        
        // if (_permissions.All(el => (int)el != (int)orgMember.Permission))
        // {
        //     SetRequestResult(context, "Ваших прав в данной организации недостаточно для выполнения данного действия.", 403);
        //     return;
        // }
        //
        // var user = orgMember.User;
        
        // TODO: Если хэшируем пароли включи это
        // Если шифруем пароли в БД
        // В данном случае у нас шифрованый пароль в БД и в клаймах, шифруются одинаково,
        // поэтому достаточно просто проверить их идентичность
        //
        // if (userPassword != user.Password)
        // {
        //     SetResult(context, "Authentication error.", 401);
        //     return;
        // }
        
        // Если не шифруем пароли в БД
        // Здесь нам нужно зашифровать пароль из БД и сравнить с его с тем, что был передан в клаймах
        
        // if (!Hasher.Validate(password, user.Password))
        // {
        //     SetRequestResult(context, "Authentication error.", 401);
        //     return;
        // }
        
        // var identity = new ClaimsIdentity(jwtToken.Claims, "Authentication");
        //
        // context.HttpContext.User = new ClaimsPrincipal(identity);

        base.OnActionExecuting(context);
    }
    
    private static void SetRequestResult(ActionExecutingContext context, string errorMessage, int statusCode)
    {
        var errorObj = new AuthenticationErrorModel()
        {
            ErrorMessage = errorMessage
        };

        context.Result = new ObjectResult(errorObj)
        {
            StatusCode = statusCode
        };
    }
}