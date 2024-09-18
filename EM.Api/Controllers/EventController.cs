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

        public EventController(IMapper mapper, IEventService eventService, IAuthService authService, IValidator<EventDTO> eventValidator)
        {
            _mapper = mapper;
            _eventService = eventService;
            _authService = authService;
            _eventValidator = eventValidator;
        }

        [Authorize(Policy = "UserPolicy")]
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

                if (eventResponse != null)
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
        /// <summary>
        /// To fetch the events based on the filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        [HttpGet("event")]
        public async Task<IActionResult> GetEvents([FromQuery] EventFilterDTO filter)
        {
            try
            {
                var events = await _eventService.GetEventsAsync(filter);
                var pagination = new PaginationMetadata(filter.Page, events.TotalRecords, filter.Size);
                var venueResponseDTO = new VenueResponseDTO();
                var eventResponseDTO = events.Events.Select(e => new EventListResponseDTO
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    BasePrice = e.BasePrice,
                    Status = e.Status.ToString(),
                    OrganizerId = e.OrganizerId,
                    PerformerId = e.PerformerId,
                    VenueId = e.VenueId,
                    CreatedOn = e.CreatedOn,
                    ModifiedOn = e.ModifiedOn,
                    StartDateTime = TimeConversionHelper.ToCustomDateTimeString(e.StartDateTime),
                    EndDateTime = TimeConversionHelper.ToCustomDateTimeString(e.EndDateTime),
                    Performer = _mapper.Map<PerformerResponseDTO>(e.Performer),
                    Venue = _mapper.Map<VenueResponseDTO>(e.Venue),
                    EventDocument = e.EventDocument.Select(ed => _mapper.Map<EventDocumentResponseDTO>(ed)).ToList(),
                    EventPriceCategories = e.EventPriceCategory.Select(epc => _mapper.Map<EventPriceCategoryResponseDTO>(epc)).ToList(),
                    Offers = e.Offer.Select(o => _mapper.Map<OfferResponseDTO>(o)).ToList()
                }).ToList();
                var response = new PagedResponseDTO<List<EventListResponseDTO>>(eventResponseDTO, "success", "Event Successfully fetched.", pagination);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }
        }

        /// <summary>
        /// Get the event details based on the Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("event/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            try
            {
                var _event = await _eventService.GetEventById(id);
                var eventListResponse = new EventListResponseDTO
                {
                    Id = _event.Id,
                    Title = _event.Title,
                    Description = _event.Description,
                    BasePrice = _event.BasePrice,
                    Status = _event.Status.ToString(),
                    OrganizerId = _event.OrganizerId,
                    PerformerId = _event.PerformerId,
                    VenueId = _event.VenueId,
                    CreatedOn = _event.CreatedOn,
                    ModifiedOn = _event.ModifiedOn,
                    StartDateTime = TimeConversionHelper.ToCustomDateTimeString(_event.StartDateTime),
                    EndDateTime = TimeConversionHelper.ToCustomDateTimeString(_event.EndDateTime),
                    Performer = _mapper.Map<PerformerResponseDTO>(_event.Performer),
                    Venue = _mapper.Map<VenueResponseDTO>(_event.Venue),
                    EventDocument = _event.EventDocument.Select(ed => _mapper.Map<EventDocumentResponseDTO>(ed)).ToList(),
                    EventPriceCategories = _event.EventPriceCategory.Select(epc => _mapper.Map<EventPriceCategoryResponseDTO>(epc)).ToList(),
                    Offers = _event.Offer.Select(o => _mapper.Map<OfferResponseDTO>(o)).ToList()
                };
                var response = new ResponseDTO<EventListResponseDTO>(eventListResponse, "success", "Event Returned Successfully", null);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }
        }
        [Authorize(Policy = "UserPolicy")]
        [HttpPut("event/{event_id}/publish")]
        public async Task<IActionResult> PublishEvent(int event_id)
        {
            try
            {
                EventBO eventBO = await _eventService.PublishEvent(event_id);
                EventResponseDTO eventResponseDTO = new EventResponseDTO();

                _mapper.Map(eventBO, eventResponseDTO);
                return Ok(new ResponseDTO<EventResponseDTO>(eventResponseDTO, "success", "Event Published."));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }

        }
    }
}