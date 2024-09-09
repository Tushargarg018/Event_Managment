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
using Microsoft.AspNetCore.StaticFiles;
using System.IdentityModel.Tokens.Jwt;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class PerformerController : ControllerBase
    {

        //private readonly ILogger<OrganizerController> _logger;
        private readonly IAuthService _authservice;
        private readonly IMapper _mapper;
        //private readonly IValidator<LoginDto> _loginValidator;
        private readonly IFileService _fileService;
        private readonly IPerformerService _performerService;
        private readonly IConfiguration _config;

        public PerformerController(IMapper mapper, IFileService fileService, IPerformerService performerService, IAuthService authservice, IConfiguration configuration)
        {
            _mapper = mapper;
            _fileService = fileService;
            _performerService = performerService;
            _authservice = authservice;
            _config = configuration;
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpPost("performer")]
        public async Task<IActionResult> AddPerformer(PerformerDTO performerDto)
        {
            var authHeader = Request.Headers.Authorization;
            var organizerId = _authservice.GetOrganizerIdFromToken(authHeader.ToString());

            if (performerDto.ImageFile?.Length > 1 * 1024 * 1024)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
            }
            string[] allowedFileExtentions = [".jpg", ".jpeg", ".png"];
            string baseUrl = _config.GetValue<string>("AppSettings:BaseUrl");
            string createdImageName = await _fileService.SaveImageAsync(performerDto.ImageFile, allowedFileExtentions, organizerId.ToString(), baseUrl);

            PerformerBO performerBo = new PerformerBO();
            _mapper.Map(performerDto, performerBo);
            performerBo.Profile = createdImageName;
            performerBo.OrganizerId = organizerId;
            var performer = _performerService.AddPerformer(performerBo);
            var response = new ResponseDTO<PerformerBO>(performer, "success", "Performer Added Successfully", null);
            return Ok(response);
        }

        private readonly string _imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(),"");

        [Authorize(Policy ="UserPolicy")]
        [HttpGet("getProfilePic")]
        public async Task<IActionResult> GetImage(string filePath)
        {
            string fileLocation = Path.Combine(_imagesDirectory, filePath);

            if (!System.IO.File.Exists(fileLocation))
            {
                return NotFound(new ResponseDTO<PerformerBO>(null, "failure", "File Not Found", null));
            }
            var image = _fileService.GetImageAsByteArray(fileLocation);
            return File(image, "application/octet-stream", filePath);
        }
    }
}
