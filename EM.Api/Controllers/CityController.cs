using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EM.Api.Validations;
using EM.Business.Repository;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IMapper mapper;
		private readonly IValidator<int> _stateIdValidator;

		public CityController(ICityService cityService , IMapper mapper,StateIdValidator stateIdValidator)
        {
            this.cityService = cityService;
            this.mapper = mapper;
		    _stateIdValidator = stateIdValidator;
		}

        /// <summary>
        /// Retrieves a list of cities based on the provided state ID.
        /// </summary>
        /// <param name="state_id">The ID of the state to filter cities by.</param>
        /// <returns>
        /// A response containing the list of cities in JSON format.
        /// - Returns `200 OK` with a success message and the list of cities if the state ID is valid and cities are found.
        /// - Returns `404 Not Found` if no state is found with the provided ID, or if no cities are found for the given state.
        /// - Returns `500 Internal Server Error` if an unexpected error occurs.
        /// </returns>

        [HttpGet("{state_id}/cities")]
        public async Task<IActionResult> GetCityList(int state_id)
        {
			var stateValid = await _stateIdValidator.ValidateAsync(state_id);
			if (!stateValid.IsValid)
			{
				return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Invalid state ID")
					.WithErrors(stateValid.Errors.Select(e => e.ErrorMessage).ToList()));
			}
			try
            {
                var cities = await cityService.GetCities(state_id);
				if (cities == null || !cities.Any())
				{
					return NotFound(new ResponseDTO<object>(Array.Empty<object>(), "success", "No cities found for the given state"));
				}
				var citiesDto = mapper.Map<List<CityDto>>(cities);
				return Ok(new ResponseDTO<List<CityDto>>(citiesDto, "success", "Cities retrieved successfully"));             


			}
            catch (Exception ex) 
            {
				return StatusCode(StatusCodes.Status500InternalServerError,
			new ResponseDTO<object>(Array.Empty<object>(), "failure", "An unexpected error occurred")
				.WithErrors(new List<string> { ex.Message }));
			}
            
        }
    }
}
