using AutoMapper;
using EM.Api.Validations;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using EM.Core.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EM.Core.Helpers;
using EM.Business.ServiceImpl;
namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly IAuthService _authService;
        private readonly IValidator<EventDTO> _eventValidator;

        public EventController(IMapper mapper, IEventService eventService, IAuthService authService, IValidator<EventDTO> eventValidator)
        {
            _mapper = mapper;
            _eventService = eventService;   
            _authService = authService;
            _eventValidator = eventValidator;
        }

        [Authorize(Policy ="UserPolicy")]
        [HttpPost("event")]
        public async Task<IActionResult> AddEvent(EventDTO eventDto)
        {
            var validationResult = await _eventValidator.ValidateAsync(eventDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation Errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            }            
            try
            {
                var authHeader = Request.Headers.Authorization;
                var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
                var eventResponse = await _eventService.AddEvent(eventDto, organizerId);

                var eventResponseDTO = new EventResponseDTO();
                _mapper.Map(eventResponse, eventResponseDTO);

                if(eventResponse != null)
                {
                    return Ok(new ResponseDTO<EventResponseDTO>(eventResponseDTO, "success", "Event Added Successfully"));
                }
                else
                {
                    return Ok(new ResponseDTO<object>(Array.Empty<object>(), "success", "Event not added."));
                }               

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }

        }
    }
}
