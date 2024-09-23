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
        [Authorize(Policy = "UserPolicy")]
        [HttpPost("event/{id:int}/offer")]
        public async Task<IActionResult> AddUpdateOffers(int id,[FromBody]OfferDTO offerDto)
        {
            offerDto.EventId = id;
            offerDto.Discount = Math.Round(offerDto.Discount, 2);
            var validationResult = await _offerValidator.ValidateAsync(offerDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation failed", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));
            }
            OfferBO offerBo = await _offerService.AddUpdateEventOffer(offerDto, id, offerDto.OfferId);
            var message = "";
            if (offerDto.OfferId == 0)
            {
                message = "Offer Created Successfully";
            }
            else
                message = "Offer Updated Successfully";
            return Ok(new ResponseDTO<OfferBO>(offerBo, "success", message, null));
        }
    }
}
