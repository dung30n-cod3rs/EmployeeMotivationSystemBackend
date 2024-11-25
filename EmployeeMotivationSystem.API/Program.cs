using System.Net;
using EmployeeMotivationSystem.API.Mapper;
using EmployeeMotivationSystem.DAL;
using Microsoft.EntityFrameworkCore;

const string connectionStringKey = "BaseStorage";
const string dataAccessLayerAssemblyName = "EmployeeMotivationSystem.DAL";

var builder = WebApplication.CreateBuilder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dbConnectionString = builder.Configuration.GetConnectionString(connectionStringKey);
if (dbConnectionString == null)
    throw new Exception("Connection string can't be null.");

builder.Services
    .AddDbContext<AppDbContext>(b 
            => b.UseNpgsql(
                dbConnectionString, 
                o => 
                    o.MigrationsAssembly(dataAccessLayerAssemblyName)),
            optionsLifetime: ServiceLifetime.Singleton);

builder.Services
    .AddDbContextFactory<AppDbContext>(e 
        => e.UseNpgsql(
            dbConnectionString, 
            sqlServerOptionsBuilder 
                => sqlServerOptionsBuilder.MigrationsAssembly(dataAccessLayerAssemblyName))
);

builder.Services.RegisterMapsterConfiguration();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();