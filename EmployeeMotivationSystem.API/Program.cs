using System.Net;
using EmployeeMotivationSystem.API.Constants;
using EmployeeMotivationSystem.API.Middleware.Exceptions;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

const string connectionStringKey = "BaseStorage";
const string dataAccessLayerAssemblyName = "EmployeeMotivationSystem.DAL";

var builder = WebApplication.CreateBuilder();

builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(IPAddress.Any, 5000);
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    opt.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new AppTokenValidationParameters());

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors(c =>
{
    c
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true == true)
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        // opt.RoutePrefix = "/api";
        // opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Name");
    });
}

app.UseMiddleware<ExceptionCatcherMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// TODO: Maybe we need call to dotnet ef database update into dockerfile?
if (!app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
        context.Database.Migrate();
}

app.Run();