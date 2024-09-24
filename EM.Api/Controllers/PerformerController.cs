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
using EM.Data.Entities;
using EM.Api.Validations;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class PerformerController : ControllerBase
    {

        //private readonly ILogger<OrganizerController> _logger;
        private readonly IAuthService _authservice;
        private readonly IMapper _mapper;
        private readonly IValidator<PerformerDTO> _performerValidator;
        private readonly IFileService _fileService;
        private readonly IPerformerService _performerService;
        private readonly IConfiguration _config;

        public PerformerController(IMapper mapper, IFileService fileService, IPerformerService performerService, IAuthService authservice, IConfiguration configuration, IValidator<PerformerDTO> performerValidator)
        {
            _mapper = mapper;
            _fileService = fileService;
            _performerService = performerService;
            _authservice = authservice;
            _config = configuration;
            _performerValidator = performerValidator;
        }


        //[Authorize(Policy = "UserPolicy")]
        [HttpPost("performer")]
        public async Task<IActionResult> AddPerformer(PerformerDTO performerDto)
        {
            var validationResult = await _performerValidator.ValidateAsync(performerDto);
            if (!validationResult.IsValid)
            {
                // Return a bad request response if validation fails
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation failed", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));

            }
            try
            {
                var performerBo = await _performerService.AddPerformer(performerDto);
                string createdImageName = await _fileService.SaveImageFromBase64(performerDto.Base64String, performerBo.Id, 3);
                await _performerService.UpdatePerformerImage(createdImageName, performerBo.Id);
                performerBo.Profile = createdImageName;
                var response = new ResponseDTO<PerformerBO>(performerBo, "success", "Performer Added Successfully", null);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }            
        }

        private readonly string _imagesDirectory = Path.Combine(Directory.GetCurrentDirectory(),"");

        //[Authorize(Policy ="UserPolicy")]
        [HttpGet("performer")]
        public async Task<IActionResult> GetPerformers()
        {
            var performerList = await _performerService.GetPerformers();
            var response = new ResponseDTO<List<PerformerBO>>(performerList, "success", "Performers Returned Successfully", null);
            return Ok(response);
        }

        /// <summary>
        /// Get a performer by it's Id
        /// </summary>
        /// <returns>Performer</returns>
        [HttpGet("performer/{id}")]
        public async Task<IActionResult> GetPerformerById(int id)
        {
            var performer = await _performerService.GetPerformerById(id);
            var response = new ResponseDTO<PerformerBO>(performer, "success", "Performer Returned Successfully", null);
            return Ok(response);
        }

        /// <summary>
        /// update the exisitng performer
        /// </summary>
        /// <param name="performerDto"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        //[Authorize(Policy ="UserPolicy")]
        [HttpPut("performer/{id}")]
        public async Task<IActionResult> UpdatePerformer(PerformerUpdateDTO performerDto, int id)
        {
            //var authHeader = Request.Headers.Authorization;
            //var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
            var existingPerformer = await _performerService.GetPerformerById(id);
            if (existingPerformer == null)
            {
                return NotFound(new { message = "Performer not found." });
            }

            var updatedImageName = existingPerformer.Profile;
            if (performerDto.Base64String != null)
            {
                updatedImageName = await _fileService.UpdateImageAsync(performerDto.Base64String, id);
            }

            PerformerBO performerBo = new PerformerBO();
            performerBo = await _performerService.UpdatePerformer(performerDto, id, updatedImageName);
            return Ok(performerBo);
        }
    }
}
