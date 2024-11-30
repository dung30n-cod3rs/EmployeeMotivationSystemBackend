﻿using EmployeeMotivationSystem.API.Models.Base;
using EmployeeMotivationSystem.API.Models.Filials;
using EmployeeMotivationSystem.DAL;
using EmployeeMotivationSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMotivationSystem.API.Controllers;

[Authorize]
public sealed class FilialsController : BaseController
{
    public FilialsController(AppDbContext dbContext) 
        : base(dbContext) { }

    [HttpGet("{id:int}")]
    public async Task<GetFilialByIdResponseApiDto> GetFilialById(int id)
    {
        var filial = await DbContext.Filials
            .Include(filial => filial.Company)
            .SingleOrDefaultAsync(el => el.Id == id);

        if (filial == null)
            throw new Exception($"Filial with id: {id} not found!");

        return new GetFilialByIdResponseApiDto
        {
            Item = new FilialApiDto
            {
                CreationDate = filial.CreationDate,
                Name = filial.Name,
                Address = filial.Address,
                Company = new CompanyApiDto
                {
                    CreationDate = filial.Company.CreationDate,
                    Name = filial.Company.Name
                }
            }
        };
    }

    [HttpGet("ByCompany")]
    public async Task<GetFilialByCompanyIdApiDto> GetFilialsByCompanyId(int companyId)
    {
        var filials = await DbContext.Filials
            .Include(el => el.Company)
            .Where(el => el.CompanyId == companyId)
            .ToArrayAsync();

        return new GetFilialByCompanyIdApiDto
        {
            Items = filials.Select(el => new FilialApiDto
            {
                CreationDate = el.CreationDate,
                Name = el.Name,
                Address = el.Address,
                Company = new CompanyApiDto
                {
                    CreationDate = el.Company.CreationDate,
                    Name = el.Company.Name
                }
            })
        };
    }
    
    [HttpPost]
    public async Task<CreateFilialResponseApiDto> CreateFilial([FromBody] CreateFilialRequestApiDto request)
    {
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");

        var newFilial = await DbContext.Filials.AddAsync(new Filial
        {
            Name = request.Name,
            Address = request.Address,
            CompanyId = company.Id
        });

        await DbContext.SaveChangesAsync();

        return new CreateFilialResponseApiDto
        {
            Item = new FilialApiDto
            {
                CreationDate = newFilial.Entity.CreationDate,
                Name = newFilial.Entity.Name,
                Address = newFilial.Entity.Address,
                Company = new CompanyApiDto
                {
                    CreationDate = company.CreationDate,
                    Name = company.Name
                }
            }
        };
    }

    [HttpPut]
    public async Task<UpdateFilialResponseApiDto> UpdateFilial([FromBody] UpdateFilialRequestApiDto request)
    {
        var filial = await DbContext.Filials
            .SingleOrDefaultAsync(el => el.Id == request.FilialId);

        if (filial == null)
            throw new Exception($"Filial with id: {request.CompanyId} not found!");
        
        var company = await DbContext.Companies
            .SingleOrDefaultAsync(el => el.Id == request.CompanyId);

        if (company == null)
            throw new Exception($"Company with id: {request.CompanyId} not found!");

        filial.Name = request.Name;
        filial.Address = request.Address;
        filial.CompanyId = company.Id;
        
        await DbContext.SaveChangesAsync();

        return new UpdateFilialResponseApiDto
        {
            Item = new FilialApiDto
            {
                CreationDate = filial.CreationDate,
                Name = filial.Name,
                Address = filial.Address,
                Company = new CompanyApiDto
                {
                    CreationDate = company.CreationDate,
                    Name = company.Name
                }
            }
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteFilial(int id)
    {
        var filial = await DbContext.Filials
            .Include(filial => filial.Company)
            .SingleOrDefaultAsync(el => el.Id == id);

        if (filial == null)
            throw new Exception($"Filial with id: {id} not found!");

        DbContext.Filials.Remove(filial);
        
        await DbContext.SaveChangesAsync();
        
        return Ok();
    }
}