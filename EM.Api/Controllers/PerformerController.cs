using AutoMapper;
using EM.Business.BOs;
using EM.Core.Helpers;
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
            var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
            string createdImageName = await _fileService.SaveImageFromBase64(performerDto.Base64String, organizerId, 3);
            //performerDto.Base64String = createdImageName;
            var performerBo = await _performerService.AddPerformer(performerDto, organizerId, createdImageName);
            var response = new ResponseDTO<PerformerBO>(performerBo, "success", "Performer Added Successfully", null);
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

        [Authorize(Policy ="UserPolicy")]
        [HttpGet("performer")]
        public async Task<IActionResult> GetPerformers()
        {
            var authHeader = Request.Headers.Authorization;
            var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
            var performerList = _performerService.GetPerformers(organizerId);
            var response = new ResponseDTO<List<PerformerBO>>(performerList, "success", "Performers Returned Successfully", null);
            return Ok(response);
        }
        /// <summary>
        /// update the exisitng performer
        /// </summary>
        /// <param name="performerDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Policy ="UserPolicy")]
        [HttpPut("performer/{id}")]
        public async Task<IActionResult> UpdatePerformer(PerformerUpdateDTO performerDto, int id)
        {
            var authHeader = Request.Headers.Authorization;
            var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
            var existingPerformer = await _performerService.GetPerformerById(id);
            if (existingPerformer == null)
            {
                return NotFound(new { message = "Performer not found." });
            }

            var updatedImageName = existingPerformer.Profile;
            if (performerDto.Base64String != null)
            {
                updatedImageName = await _fileService.UpdateImageAsync(performerDto.Base64String, organizerId, id);
            }

            PerformerBO performerBo = new PerformerBO();
            performerBo = await _performerService.UpdatePerformer(performerDto, id, updatedImageName);
            //performerBo.Profile = $"{Request.Scheme}://{Request.Host}/{performerBo.Profile}";
            return Ok(performerBo);
        }
    }
}
