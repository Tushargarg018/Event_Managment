
using EM.Api.Mapper;
using EM.Business.Services;
using EM.Api;
using EM.Api.Validations;
using EM.Business.ServiceImpl;
using EM.Core.DTOs.Request;
using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using EM.Data.Repository;
using EM.Data.RepositoryImpl;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.

//State 
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<IStateService, StateService>();
builder.Services.AddScoped<ICityService, CityService>();

//City
builder.Services.AddScoped<ICityRepository, CityRepository>();
//Venue
builder.Services.AddScoped<IVenueService, VenueService>();
builder.Services.AddScoped<IVenueRepository, VenueRepository>();
//Event Documents
builder.Services.AddScoped<IEventDocumentRepository , EventDoucumentRepository>();
builder.Services.AddScoped<IEventDocumentService , EventDocumentService>();

// Load appsettings.json and environment-specific configurations
Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
	.AddEnvironmentVariables();

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DbCon")));

//Adding Dependencies

//Auth and Organizer
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrganizerRepository, OrganizerRepository>();

//File Services and Performer
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IPerformerService, PerformerService>();
builder.Services.AddScoped<IPerformerRepository, PerformerRepository>();

//Event
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventService, EventService>();

//Venue
builder.Services.AddScoped<IVenueRepository, VenueRepository>();

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();

//Fluent validation
//builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
builder.Services.AddValidatorsFromAssembly(typeof(LoginValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(EventValidator).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); 
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


builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("UserPolicy", policy =>
		policy.RequireClaim("Id"));
});


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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/uploads"
});

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
