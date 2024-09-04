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
    [Route("api/em")]
    public class apiController : ControllerBase
    {
        private readonly ILogger<apiController> _logger;
        private readonly IAuthService authservice;
        private readonly IMapper _mapper;

        public apiController(ILogger<apiController> logger, IAuthService repo, IMapper mapper)
        {
            _logger = logger;
            authservice = repo;
            _mapper = mapper;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(invalid);
            }
            var response = authservice.OrganizerLogin(loginDto);
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
