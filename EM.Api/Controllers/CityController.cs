using AutoMapper;
using EM.Business.Repository;
using EM.Core.DTOs.Response.Success;
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

        public CityController(ICityService cityService , IMapper mapper)
        {
            this.cityService = cityService;
            this.mapper = mapper;
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
        //[Route("api/em/{state_id}/cities")]
        public async Task<IActionResult> GetCityList(int state_id)
        {
            try
            {
                var cities = await cityService.GetCities(state_id);
                if (cities == null)
                {

                    return NotFound(new
                    {
                        status = "success",
                        message = "no state found",
                        data = (object)null
                    });

                }
                var citiesdto = new List<CityDto>();
                mapper.Map(cities, citiesdto);
                Console.WriteLine(citiesdto);
                if(!citiesdto.Any()){
                    return NotFound(new
                    {
                        status = "success",
                        message = "No cities found for the given state",
                        data = (object)null
                    });
                }
                else
                {
                    return Ok(new
                    {
                        status = "success",
                        message = "cities retrieved successfully",
                        data = citiesdto
                    });
                }
                
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "failure",
                    message = "An unexpected error occured",
                    data = (object)null
                });
            }
            
        }
    }
}
