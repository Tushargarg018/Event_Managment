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
using EM.Data.Entities;
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
        private readonly IValidator<EventUpdateDTO> _eventUpdateValidator;
        private readonly EventMapper _eventMapper;

        public EventController(IMapper mapper, IEventService eventService, IAuthService authService, IValidator<EventDTO> eventValidator, IValidator<EventUpdateDTO> eventUpdateValidator, EventMapper eventMapper)
        {
            _mapper = mapper;
            _eventService = eventService;
            _authService = authService;
            _eventValidator = eventValidator;
            //_eventUpdateValidator = eventUpdateValidator;
            _eventMapper = eventMapper;
        }
        /// <summary>
        /// Add the event
        /// </summary>
        /// <param name="eventDto"></param>
        /// <returns></returns>
        [Authorize(Policy = "UserPolicy")]
        [HttpPost("event")]
        public async Task<IActionResult> AddEvent(EventDTO eventDto)
         {
            var validationResult = await _eventValidator.ValidateAsync(eventDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation Errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            }
            var authHeader = Request.Headers.Authorization;
            var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
            var eventResponse = await _eventService.AddEvent(eventDto, organizerId);
            var eventResponseDTO = _mapper.Map<EventResponseDTO>(eventResponse);
            return Ok(new ResponseDTO<EventResponseDTO>(eventResponseDTO, "success", "Event Added Successfully"));        
        }
        [Authorize(Policy ="UserPolicy")]
        [HttpPost("event/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] EventDTO eventDto)
        {
            var validationResult = await _eventValidator.ValidateAsync(eventDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation Errors", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            }
            var authHeader = Request.Headers.Authorization;
            var organizerId = JwtTokenHelper.GetOrganizerIdFromToken(authHeader.ToString());
            var eventUpdateResponse = await _eventService.UpdateEvent(eventDto, id, organizerId);
            var eventResponseDTO = _mapper.Map<EventResponseDTO>(eventUpdateResponse);
            return Ok(new ResponseDTO<EventResponseDTO>(eventResponseDTO, "success", "Event Updated Successfully"));
        }
        /// <summary>
        /// To fetch the events based on the filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        [HttpGet("event")]
        public async Task<IActionResult> GetEvents([FromQuery] EventFilterDTO filter)
        {

            var events = await _eventService.GetEventsAsync(filter);
            var pagination = new PaginationMetadata(filter.Page, events.TotalRecords, filter.Size);
            var eventResponseDTO = _eventMapper.MapToEventListResponse(events.Events);
            var response = new PagedResponseDTO<List<EventListResponseDTO>>(eventResponseDTO, "success", "Event Successfully fetched.", pagination);
            return Ok(response);

        }

        /// <summary>
        /// Get the event details based on the Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var _event = await _eventService.GetEventById(id);
            var eventListResponse = _eventMapper.MapToEventResponse(_event);
            var response = new ResponseDTO<EventListResponseDTO>(eventListResponse, "success", "Event Returned Successfully", null);
            return Ok(response);
        }
        /// <summary>
        /// Publish existing event by Id
        /// </summary>
        /// <param name="event_id"></param>
        /// <returns></returns>
        [Authorize(Policy = "UserPolicy")]
        [HttpPut("event/{event_id}/publish")]
        public async Task<IActionResult> PublishEvent(int event_id)
        {
            EventBO eventBO = await _eventService.PublishEvent(event_id);
            EventResponseDTO eventResponseDTO = new EventResponseDTO();
            _mapper.Map(eventBO, eventResponseDTO);
            return Ok(new ResponseDTO<EventResponseDTO>(eventResponseDTO, "success", "Event Published."));
        }
    }
}