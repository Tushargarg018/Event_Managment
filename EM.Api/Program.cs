using EM.Api.Mapper;
using EM.Business.Repository;
using EM.Business.Services;
using EM.Api;
using EM.Api.Validations;
using EM.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using EM.Business.ServiceImpl;
using EM.Data.Repositories;
using EM.Data.RepositoryImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICityService , CityService>();

// Load appsettings.json and environment-specific configurations
Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DbCon")));
//Adding Dependencies

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();

//File Services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPerformerService, PerformerService>();
builder.Services.AddScoped<IPerformerRepository, PerformerRepository>();

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

//Fluent validation
//builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
        });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Description = "Enter Token starting with Bearer",
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});
	options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Name = "Bearer",
				In = ParameterLocation.Header,
				Reference = new OpenApiReference
				{
					Id = "Bearer",
					Type = ReferenceType.SecurityScheme
				}
			},
			new List<string>()
		}
	});
});


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


builder.Services.AddAuthorizationBuilder()
	.AddPolicy("UserPolicy", policy =>
	policy.RequireClaim("Id"));


var app = builder.Build();

//Configuring Exception Middleware
//app.ConfigureExceptionHandler();
app.UseMiddleware<GlobalExceptionMiddleware>();

// mapping Uploads folder to Resources folder 
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/Resources"
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
