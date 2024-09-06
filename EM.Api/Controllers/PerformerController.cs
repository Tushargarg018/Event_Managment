using AutoMapper;
using EM.Business.BOs;
using EM.Business.ServiceImpl;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class PerformerController : ControllerBase
    {

        //private readonly ILogger<OrganizerController> _logger;
        //private readonly IAuthService authservice;
        private readonly IMapper _mapper;
        //private readonly IValidator<LoginDto> _loginValidator;
        private readonly IFileService _fileService;
        private readonly IPerformerService _performerService;

        public PerformerController(IMapper mapper, IFileService fileService, IPerformerService performerService)
        {
            _mapper = mapper;
            _fileService = fileService;
            _performerService = performerService;
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpPost("performer")]
        public async Task<IActionResult> AddPerformer(PerformerDTO performerDto)
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers.Authorization;
            var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var organizerClaim = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int organizerId = int.Parse(organizerClaim);

            if (performerDto.ImageFile?.Length > 1 * 1024 * 1024)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
            }
            string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
            string createdImageName = await _fileService.SaveImageAsync(performerDto.ImageFile, allowedFileExtentions, organizerId.ToString());

            PerformerBO performerBo = new PerformerBO();
            _mapper.Map(performerDto, performerBo);
            performerBo.Profile = createdImageName;
            performerBo.OrganizerId = organizerId;
            var performer = _performerService.AddPerformer(performerBo);
            var response = new ResponseDTO<PerformerBO>(performer, "Success", "Performer Added Successfully", null);
            return Ok(response);
        }
    }
}
