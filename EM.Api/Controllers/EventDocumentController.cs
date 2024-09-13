using AutoMapper;
using EM.Api.Validations;
using EM.Business.BOs;
using EM.Business.ServiceImpl;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class EventDocumentController : ControllerBase
    {
        
        private readonly IEventDocumentService eventDocumentService;
        private readonly IMapper mapper;
        private readonly IValidator<EventDocumentRequestDTO> Documentvalidator;
        private readonly IFileService fileService;
        public EventDocumentController(IEventDocumentService eventDocumentService , IMapper mapper , IValidator<EventDocumentRequestDTO> Documentvalidator , IFileService fileService)
        {
            this.eventDocumentService = eventDocumentService;
            this.mapper = mapper;
            this.Documentvalidator = Documentvalidator;
            this.fileService = fileService;
        }
        /// <summary>
        /// Add/ Update the event documents
        /// </summary>
        /// <param name="eventDocument"></param>
        /// <param name="EventId"></param>
        /// <returns></returns>
        [Authorize(Policy ="UserPolicy")]
        [HttpPost("event/{EventId}/document")]
        public async Task<IActionResult> AddorUpdateEventDocument(EventDocumentRequestDTO eventDocument , int EventId)
        {

            var validationResult = await Documentvalidator.ValidateAsync(eventDocument);
            if (!validationResult.IsValid)
            { 
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation failed", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            }
            var eventDocumentPath = await fileService.UploadEventDocument(eventDocument.ImageFile,EventId, (int)eventDocument.Type);

            try
            {

                EventDocumentBO eventbo = eventDocument.Id == null
                ? await eventDocumentService.AddEventDocuments(eventDocument, EventId, eventDocumentPath)
                : await eventDocumentService.UpdateEventDocuments(eventDocument, EventId, eventDocumentPath);

                if (eventbo == null)
                {
                    return Ok(new ResponseDTO<object>(Array.Empty<object>(), "success", "No event/document id exist to add documents"));
                }

                var eventResponse = mapper.Map<EventDocumentResponseDTO>(eventbo);
                eventResponse.FilePath = $"{Request.Scheme}://{Request.Host}/{eventResponse.FilePath}";

                var message = eventDocument.Id == null ? "Event document added successfully" : "Event document updated successfully";
                return Ok(new ResponseDTO<EventDocumentResponseDTO>(eventResponse, "success", message));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<Object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }

        }

    }
}
