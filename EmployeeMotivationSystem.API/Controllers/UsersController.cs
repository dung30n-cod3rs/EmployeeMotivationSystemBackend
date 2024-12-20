﻿using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Users;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
public sealed class UsersController : BaseController
{
    public UsersController(AppDbContext dbContext) 
        : base(dbContext) { }
    
    [HttpGet("{id:int}")]
    public async Task<GetUserByIdResponseApiDto> GetUserById(int id)
    {
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == id);

        if (user == null)
            throw new Exception($"User with id: {id} not found!");

        int? companyId;
        
        // Чел админ
        
        var companiesByUser = await DbContext.Companies
            .FirstOrDefaultAsync(el => el.CreatorUserId == id);

        if (companiesByUser != null)
        {
            companyId = companiesByUser.Id;
            
            return new GetUserByIdResponseApiDto
            {
                Item = new UserApiDto
                {
                    Id = user.Id,
                    CreationDate = user.CreationDate,
                    Name = user.Name,
                    Email = user.Email,
                    CompanyId = companyId.Value
                }
            };
        }
        
        // Чел мембер
        
        var userCompany = await DbContext.CompaniesUsers
            .SingleOrDefaultAsync(el => el.UserId == id);

        if (userCompany == null)
            throw new Exception($"User with id: {id} exits but its company not, why?");
        
        return new GetUserByIdResponseApiDto
        {
            Item = new UserApiDto
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                Name = user.Name,
                Email = user.Email,
                CompanyId = userCompany.CompanyId
            }
        };
    }
    
    [HttpPost("Metrics")]
    public async Task<GetUserMetricsByIdResponseApiDto> GetUserMetricsById(GetUserMetricsByIdRequestApiDto request)
    {
        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == request.UserId);

        if (user == null)
            throw new Exception($"User with id: {request.UserId} not found!");

        var companiesUsersMetrics = await DbContext.CompaniesUsersMetrics
            .Include(el => el.Member)
            .Include(el => el.Metric)
            .Where(el => el.Member.UserId == user.Id)
            .Where(el => el.CreationDate > request.DateFrom)
            .Where(el => el.CreationDate < request.DateTo)
            .ToArrayAsync();

        var anyCompanyUser = companiesUsersMetrics.FirstOrDefault();

        if (anyCompanyUser == null)
        {
            return new GetUserMetricsByIdResponseApiDto
            {
                Items = companiesUsersMetrics.Select(el =>
                    new GetUserMetricsByIdResponseApiDto.GetUserMetricsByIdResponseItemApiDto()
                    {
                        MetricId = el.MetricId,
                        MetricName = el.Metric.Name,
                        MetricWeight = el.Metric.Weight,
                        MetricDescription = el.Metric.Description,
                        MetricTargetValue = el.Metric.TargetValue,

                        Count = 0,
                        Bonuses = 0
                    })
            };
        }

        var metricsCount = companiesUsersMetrics.Length;

        return new GetUserMetricsByIdResponseApiDto
        {
            Items = companiesUsersMetrics.Select(el =>
                new GetUserMetricsByIdResponseApiDto.GetUserMetricsByIdResponseItemApiDto()
                {
                    MetricId = el.MetricId,
                    MetricName = el.Metric.Name,
                    MetricWeight = el.Metric.Weight,
                    MetricDescription = el.Metric.Description,
                    MetricTargetValue = el.Metric.TargetValue,

                    Count = metricsCount,
                    Bonuses = CalculateBonuses(anyCompanyUser.Member.Salary, companiesUsersMetrics.Select(met => met.Metric), metricsCount)
                })
        };
    }
    
    [HttpPost("ChangePassword")]
    public async Task<IActionResult> ChangeUserPassword([FromBody] ChangeUserPasswordRequestApiDto request)
    {
        var isRefreshTokenExists = Request.Cookies.TryGetValue(AuthController.CookieRefreshTokenName, out var outValue);

        if (!isRefreshTokenExists)
            throw new Exception("Bad cookie refresh token!");

        var adminCompanyUserRefreshToken = await DbContext.RefreshTokens
            .Include(el => el.User)
            .FirstOrDefaultAsync(el => el.Token == outValue);

        if (adminCompanyUserRefreshToken == null)
            throw new Exception("Maybe session of company admin is expired!");

        var company = await DbContext.Companies
                .SingleOrDefaultAsync(el => el.CreatorUserId == adminCompanyUserRefreshToken.UserId);

        if (company == null)
            throw new Exception("Maybe you are not a admin of company!");

        if (request.Password != request.RepeatPassword)
            throw new Exception("Password and RepeatPassword fields are not equal!");

        var user = await DbContext.Users
            .SingleOrDefaultAsync(el => el.Id == request.Id);

        if (user == null)
            throw new Exception($"User with id: {request.Id} not found!");

        user.Password = request.Password;

        await DbContext.SaveChangesAsync();
        
        return Ok();
    }
    
    // TODO: Move to BLL 100%
    public static double CalculateBonuses(double userSalary, IEnumerable<Metric> metricsForCalculation, int userMetricsCount)
    {
        var arrayedMetrics = metricsForCalculation.ToArray();
        
        var totalBonus = 0d;

        var maxBonus = userSalary * 0.5;

        foreach (var metric in arrayedMetrics)
        {
            var metricScore = CalculateMetricScore(metric.TargetValue, userMetricsCount);
            var weightedScore = metricScore * metric.Weight;
            totalBonus += weightedScore;
        }

        var normalizedBonuses = (totalBonus / 100) * maxBonus;

        return normalizedBonuses;
    }

    public static double CalculateMetricScore(double metricTargetValue, int actualValue)
    {
        var score = (actualValue / metricTargetValue) * 100;
        
        return Math.Min(score, 150);
    }
}