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


        [HttpPost("event/{EventId}/document")]
        public async Task<IActionResult> AddorUpdateEventDocument(EventDocumentRequestDTO eventDocument , int EventId)
        {

            if(eventDocument.ImageFile.Length > 1 * 1024 * 1024)
            {
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "image size greater than 1mb"));
            }

            var validationResult = await Documentvalidator.ValidateAsync(eventDocument);
            if (!validationResult.IsValid)
            { 
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation failed", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            }

            string[] allowedFileExtensions = [".jpg", ".jpeg", ".png"];
            string folderPath = eventDocument.Type == 0
                ? "Uploads/EventDocuments/Logo"
                : "Uploads/EventDocuments/Banner";

            var createdImageName = await fileService.SaveImage(eventDocument.ImageFile, allowedFileExtensions, EventId, folderPath);

            try
            {

                EventDocumentBO eventbo = null;
                if (eventDocument.Id == null)
                {
                    eventbo = await eventDocumentService.AddEventDocuments(eventDocument, EventId , createdImageName);
                }
                else
                {
                    eventbo = await eventDocumentService.UpdateEventDocuments(eventDocument, EventId , createdImageName);
                }

                EventDocumentResponseDTO eventResponse = new EventDocumentResponseDTO();

                mapper.Map(eventbo, eventResponse);
                eventResponse.FilePath = $"{Request.Scheme}://{Request.Host}/{eventResponse.FilePath}"; 
                
                if (eventResponse == null)
                {

                    return Ok(new ResponseDTO<object>(Array.Empty<object>(), "success", "No event/document id  exist to add documents "));
                }
                
                if (eventDocument.Id == null)
                {
                    return Ok(new ResponseDTO<EventDocumentResponseDTO>(eventResponse, "success", "event document added successfully"));
                }
                else
                {
                    return Ok(new ResponseDTO<EventDocumentResponseDTO>(eventResponse, "success", "event document updated successfully"));
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<Object>(), "failure", "An unexpected error occurred", new List<string> { ex.Message }));
            }

        }

    }
}
