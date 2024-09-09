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
                var errorList = validationResult.Errors;
                List<string> errors = new List<string>();
                foreach (var error in errorList)
                {
                    errors.Add(error.ErrorMessage);
                }
                //return Ok(errors);
                return BadRequest(new ResponseDTO<LoginResponseDTO>(null, "failure", "Validation Errors", errors));
            }

            var authHeader = Request.Headers.Authorization;
            var organizerId = _authService.GetOrganizerIdFromToken(authHeader.ToString());
            var eventBo = new EventBO();
            eventBo.OrganizerId = organizerId;
            _mapper.Map(eventDto, eventBo);
            var responseEventBo = _eventService.AddEvent(eventBo);
            var response = new ResponseDTO<EventBO>(responseEventBo, "success", "Event Added Succesfully", null);
            return Ok(response);
        }
    }
}
