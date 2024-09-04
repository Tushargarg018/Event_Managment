using AutoMapper;
using EM.Business.BOs.Request;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response.Errors;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EM.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthService authservice;
        private readonly IMapper _mapper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuthService repo, IMapper mapper)
        {
            _logger = logger;
            authservice = repo;
            _mapper = mapper;
        }
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }       

        /// <summary>
        /// This Controller Logs in the organizer with the requirements of email and password.
        /// LoginDto is Converted to LoginBo which then is returned as LoginResponseBo which is again converted to LoginResponseDto
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            LoginBo loginBo = new LoginBo();
            _mapper.Map(loginDto, loginBo);
            InvalidCredentialsDto invalid = new InvalidCredentialsDto();
            invalid.status = "Error";
            invalid.message = "Invalid Credentials";
            if (!ModelState.IsValid)
            {
                return BadRequest(invalid);
            }
            var response = authservice.OrgLogin(loginBo);
            if (response == null)
            {
                return Unauthorized(invalid);
            }
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            _mapper.Map(response, loginResponseDto);
            return Ok(loginResponseDto);
        }

        [Authorize(Policy ="UserPolicy")]
        [HttpPost("GetList")]
        public IActionResult GetList()
        {
            var token = Request.Headers.Authorization;
            return Ok(token);
        }

    }
}
