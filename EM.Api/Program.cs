<<<<<<< HEAD
using EM.Api.Mapper;
using EM.Business.Repository;
using EM.Business.Services;
=======
using EM.Api;
using EM.Api.Validations;
using EM.Business;
using EM.Business.Services;
using EM.Core.DTOs.Request;
>>>>>>> 8bbd26dc4674baacd877b0e205f5f09211f6d01f
using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

<<<<<<< HEAD
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICityService , CityService>();
=======

>>>>>>> 8bbd26dc4674baacd877b0e205f5f09211f6d01f

// Load appsettings.json and environment-specific configurations
Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DbCon")));

<<<<<<< HEAD
=======
//Adding Dependencies

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();

//Register AutoMapper
>>>>>>> 8bbd26dc4674baacd877b0e205f5f09211f6d01f
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

//Fluent validation
//builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);
//


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD
=======

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	
}).AddJwtBearer(options =>
{
	options.IncludeErrorDetails = true;
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = Configuration["Jwt:Issuer"],
		ValidAudience = Configuration["Jwt:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
	};
});


builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("UserPolicy", policy =>
		policy.RequireClaim("Id"));
});


>>>>>>> 8bbd26dc4674baacd877b0e205f5f09211f6d01f
var app = builder.Build();

//Configuring Exception Middleware
//app.ConfigureExceptionHandler();
app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
