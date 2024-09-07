using AutoMapper;
using EM.Business.BOs.Request;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Errors;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            InvalidCredentialsDto invalid = new InvalidCredentialsDto();
            invalid.status = "Error";
            invalid.message = "Invalid Credentials";
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
                return BadRequest(new ResponseDTO<LoginResponseDto>(null, "Failure", "Validation Errors", errors));
            }
            var response = authservice.OrganizerLogin(loginDto);
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            _mapper.Map(response, loginResponseDto);
            var resultResponse = new ResponseDTO<LoginResponseDto>(loginResponseDto, "Success", "Organizer Login Successful", null);
            return Ok(resultResponse);
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
