using AutoMapper;
using EM.Business.BOs;
using EM.Business.Repository;
using EM.Core.DTOS.Response.Success;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService stateService;
        private readonly IMapper mapper;

        public StateController(IStateService stateService , IMapper mapper)
        {
            this.stateService = stateService;
            this.mapper = mapper;

        }
        /// <summary>
        /// Retrieves a list of states based on the provided country ID.
        /// </summary>
        /// <param name="CountryId">The ID of the country to filter states by. If null, states for country ID 1 will be retrieved.</param>
        /// <returns>
        /// A response containing the list of states in JSON format. 
        /// - Returns `200 OK` with a success message and the list of states if the country ID is valid.
        /// - Returns `404 Not Found` with an appropriate message if no states are found or if the country ID is not available.
        /// - Returns `500 Internal Server Error` if an unexpected error occurs.
        /// </returns>
        [HttpGet("states")]
        //[Route("api/em/venue/states")]
        public async Task<IActionResult> GetState(int? CountryId)
        {
            try
            {
                if (CountryId == 1 || CountryId == null)
                {
                    var states = await stateService.GetStates(1);
                    var statedto = new List<StateDto>();
                    mapper.Map(states, statedto);
                    if (statedto == null)
                    {
                        return NotFound(new
                        {
                            status = "success",
                            message = "states not found",
                            data = (object)null
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            status = "success",
                            message = "states retrieved successfully",
                            data = statedto
                        }
                    );
                    }
                    
                }
                else
                {
                    return NotFound(new
                    {
                        status = "success",
                        message = "country not available",
                        data = (object)null
                    });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "error",
                    message = "An unexpected error occurred.",
                    data = (object)null
                });

            }
            
            
            
        }

    }
}
