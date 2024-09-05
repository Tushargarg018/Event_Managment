using AutoMapper;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EM.Api.Controllers
{
    [ApiController]
    [Route("api/em")]
    public class apiController : ControllerBase
    {
        private readonly ILogger<apiController> _logger;
        private readonly IAuthService authservice;
        private readonly IMapper _mapper;
        private readonly IValidator<LoginDto> _loginValidator;

        public apiController(ILogger<apiController> logger, IAuthService repo, IMapper mapper, IValidator<LoginDto> loginValidator)
        {
            _logger = logger;
            authservice = repo;
            _mapper = mapper;
            _loginValidator = loginValidator;
        }
              

        /// <summary>
        /// This Controller Logs in the organizer with the requirements of email and password.
        /// LoginDto is Converted to LoginBo which then is returned as LoginResponseBo which is again converted to LoginResponseDto
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var validationResult = _loginValidator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                var errorList= validationResult.Errors;
                List<string> errors = new List<string>();
                foreach (var error in errorList)
                {
                    errors.Add(error.ErrorMessage);
                }
                //return Ok(errors);
                return BadRequest(new ResponseDTO<LoginResponseDTO>(null, "Failure", "Validation Errors", errors));
            }
            var response = authservice.OrganizerLogin(loginDto);
            LoginResponseDTO loginResponseDto = new LoginResponseDTO();
            _mapper.Map(response, loginResponseDto);
            var resultResponse = new ResponseDTO<LoginResponseDTO>(loginResponseDto, "Success", "Organizer Login Successful", null);
            return Ok(resultResponse);
        }

        [Authorize(Policy ="UserPolicy")]
        [HttpPost("performer")]
        public IActionResult AddPerformer(PerformerDTO performerDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers.Authorization;
            var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var organizerId = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int org = int.Parse(organizerId);
            return Ok(org);
        }
    }
}
