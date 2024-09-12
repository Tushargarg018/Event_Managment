using AutoMapper;
using EM.Api.Validations;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOs.Response;
using EM.Core.Enums;
using EM.Core.Helpers;
using EM.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOfferService _offerService;
        private readonly IValidator<OfferDTO> _offerValidator;
        public OfferController(IMapper mapper, IOfferService offerService, IValidator<OfferDTO> offerValidator)
        {
            _mapper = mapper;
            _offerService = offerService;
            _offerValidator = offerValidator;
        }
        //[Authorize(Policy = "UserPolicy")]
        [HttpPost("{id:int}/offer")]
        public async Task<IActionResult> AddUpdateOffers(int id,[FromBody]OfferDTO offerDto)  //id: event id
        {
            offerDto.EventId = id;
            var validationResult = await _offerValidator.ValidateAsync(offerDto);
            if (!validationResult.IsValid)
            {
                var errorList = validationResult.Errors;
                List<string> errors = new List<string>();
                foreach (var error in errorList)
                {
                    errors.Add(error.ErrorMessage);
                }
                return BadRequest(new ResponseDTO<LoginResponseDTO>(null, "failure", "Validation Errors", errors));
            }
            EventOffer eventOffer = new EventOffer();
            _mapper.Map(offerDto, eventOffer);
            eventOffer.EventId = id;
            eventOffer.TotalOffers = offerDto.Quantity;
            eventOffer.GroupSize = offerDto.GroupSize;
            if (offerDto.OfferId == 0)
            {
                eventOffer = _offerService.AddEventOffer(eventOffer, id);
            }
            else
            {
                eventOffer = _offerService.UpdateEventOffer(eventOffer, id, offerDto.OfferId);
            }
            OfferBO offerBo = new OfferBO();
            _mapper.Map(eventOffer, offerBo);
            offerBo.CreatedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.ConvertTimeFromUTC(offerBo.ModifiedOn);
            offerBo.CreatedOn = TimeConversionHelper.TruncateSeconds(offerBo.CreatedOn);
            offerBo.ModifiedOn = TimeConversionHelper.TruncateSeconds(offerBo.ModifiedOn);
            var message = "";
            if (offerDto.OfferId == 0)
            {
                message = "Offer Created Successfully";
            }
            else
                message = "Offer Updated Successfully";
            return Ok(new ResponseDTO<OfferBO>(offerBo, "sucess", message, null));
        }

    }
}
