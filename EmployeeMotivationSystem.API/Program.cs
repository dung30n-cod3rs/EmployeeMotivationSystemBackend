using EmployeeMotivationSystem.API.Constants;
using EmployeeMotivationSystem.API.Middleware.Exceptions;
using EmployeeMotivationSystem.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AppAuthOptions.Issuer,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AppAuthOptions.Audience,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AppAuthOptions.SymmetricSecurityKey,
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });


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
if (app.Environment.IsDevelopment())
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

app.Run();