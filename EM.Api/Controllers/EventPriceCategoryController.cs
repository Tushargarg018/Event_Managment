using AutoMapper;
using EM.Business.BOs;
using EM.Business.ServiceImpl;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class EventPriceCategoryController : ControllerBase
    {
        private readonly IEventPriceCategoryService eventPriceCategoryService;
        private readonly IMapper mapper;
        private readonly IValidator<EventPriceCategoryRequestDTO> validator;

        public EventPriceCategoryController(IEventPriceCategoryService eventPriceCategoryService , IMapper mapper , IValidator<EventPriceCategoryRequestDTO> validator)
        {
            this.eventPriceCategoryService = eventPriceCategoryService;
            this.mapper = mapper;
            this.validator = validator;
        }

        /// <summary>
        /// To add the ticket category for the existing event
        /// </summary>
        /// <param name="eventPriceCategoryRequestDTO"></param>
        /// <param name="EventId"></param>
        /// <returns></returns>
        [Authorize(Policy = "UserPolicy")]
        [HttpPost("event/{EventId}/event_price_category")]
        public async Task<IActionResult> AddorUpdateEventPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO , int EventId)
        {
            eventPriceCategoryRequestDTO.EventId = EventId;
            var validation = await validator.ValidateAsync(eventPriceCategoryRequestDTO);
            if (!validation.IsValid)
            {
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>() , "failure" , "Validation failed", validation.Errors.Select(e => e.ErrorMessage).ToList()));
            }
            EventPriceCategoryBO eventPriceCategoryBO = await eventPriceCategoryService.AddorUpdateEventPriceCategory(eventPriceCategoryRequestDTO);
             EventPriceCategoryResponseDTO eventPriceCategoryResponseDTO = new EventPriceCategoryResponseDTO();
             mapper.Map(eventPriceCategoryBO, eventPriceCategoryResponseDTO);
             return Ok(new ResponseDTO<EventPriceCategoryResponseDTO>(eventPriceCategoryResponseDTO, "success", "Price Category set successfully"));
        }

    }
}


