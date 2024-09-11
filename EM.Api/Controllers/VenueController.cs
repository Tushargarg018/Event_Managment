using AutoMapper;
using EM.Api.Validations;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace EM.Api.Controllers
{
    [Route("api/em")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IVenueService venueService; // Service layer for venue operations
        private readonly IMapper mapper; // AutoMapper instance for mapping entities to DTOs
        private readonly IValidator<VenueRequestDTO> venueRequestValidator; // FluentValidation instance for validating the venue request DTO
        private readonly IValidator<VenueUpdateDTO> venueUpdateValidator;
        //private readonly IValidator<VenueUpdatetDTO> venueRequestValidator;
        // Constructor to initialize the VenueController with dependencies
        public VenueController(IVenueService venueService, IMapper mapper, IValidator<VenueRequestDTO> venueRequestValidator , IValidator<VenueUpdateDTO> venueUpdateValidator)
        {
            this.venueService = venueService;
            this.mapper = mapper;
            this.venueRequestValidator = venueRequestValidator;
            this.venueUpdateValidator = venueUpdateValidator;
            
        }

        /// <summary>
        /// Adds a new venue to the database.
        /// </summary>
        /// <param name="venueRequestDTO">Venue request data transfer object (DTO)</param>
        /// <returns>IActionResult with the status of the operation</returns>
        [HttpPost("venue")]
        public async Task<IActionResult> AddVenue(VenueRequestDTO venueRequestDTO)
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers.Authorization;
            var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var organizerClaim = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int organizerId = int.Parse(organizerClaim);


            // Validate the incoming request DTO
            var validationResult = await venueRequestValidator.ValidateAsync(venueRequestDTO);
            if (!validationResult.IsValid)
            {
                // Return a bad request response if validation fails
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>(), "failure", "Validation failed", validationResult.Errors.Select(e => e.ErrorMessage).ToList()));

            }
            try
            {

                var venueResponse = await venueService.AddVenue(venueRequestDTO , organizerId);
                var venueResponseDTO = new VenueResponseDTO();
                mapper.Map(venueResponse, venueResponseDTO); 

                if (venueResponse == null)
                {
                  return Ok(new ResponseDTO<object>(Array.Empty<object>(), "success", "No venue added yet"));
                }

                return Ok(new ResponseDTO<VenueResponseDTO>(venueResponseDTO, "success", "Venue added successfully"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<Object>(Array.Empty<Object>(), "failure" , "An unexpected error occurred" , new List<string> { ex.Message}));
            }
        }







        /// <summary>
        /// Retrieves a list of all venues.
        /// </summary>
        /// <returns>IActionResult with the list of venues</returns>
        [HttpGet("venue")]
        public async Task<IActionResult> GetVenues()
        {

            var handler = new JwtSecurityTokenHandler();
            var authHeader = Request.Headers.Authorization;
            var token = authHeader.ToString().Substring("Bearer ".Length).Trim();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            var organizerClaim = claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            int organizerId = int.Parse(organizerClaim);

            try
            {
				var venueResponse = await venueService.GetAllVenue(organizerId);
				var venueResponseDTO = mapper.Map<List<VenueResponseDTO>>(venueResponse);

				return Ok(new ResponseDTO<List<VenueResponseDTO>>(venueResponseDTO, "success", "Venue list retrieved successfully"));   

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError , new ResponseDTO<Object>(Array.Empty<object>() , "failure", "An unexpected error occurred" , new List<string> { ex.Message}));
            }
        }

        /// <summary>
        /// Retrieves a specific venue by its ID.
        /// </summary>
        /// <param name="VenueId">The ID of the venue to retrieve</param>
        /// <returns>IActionResult with the venue data</returns>
        [HttpGet("venue/{VenueId}")]
        public async Task<IActionResult> GetVenue(int venueId)
        {
            try
            {
                // Retrieve the venue by ID using the service layer
                var venueResponse = await venueService.GetVenue(venueId);
                var venueResponseDTO = new VenueResponseDTO();
                mapper.Map(venueResponse, venueResponseDTO); 

                if (venueResponse == null)
                {
                    return Ok(new ResponseDTO<Object>(Array.Empty<object>(), "success", "no venue exist"));
                }
                // Return a success response with the venue data
                

                return Ok(new ResponseDTO<VenueResponseDTO>(venueResponseDTO, "success", "Venue retrieved successfully"));

            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response in case of an exception
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<object>(Array.Empty<object>(), "failure", "An unexpected error occurred" , new List<string> { ex.Message}));

            }
        }




        /// <summary>
        /// RUpdate a specific venue by its ID.
        /// </summary>
        /// <param name="VenueId">The ID of the venue to retrieve</param>
        /// <param name="venueRequestDTO">Venue request data transfer object (DTO)</param>
        /// <returns>IActionResult with the updated venue data</returns>

        [HttpPut("venue/{VenueId}")]
        public async Task<IActionResult> UpdateVenue(VenueUpdateDTO venueUpdateDTO , int VenueId)
        {

            

            // Validate the incoming request DTO
            var validationResult = await venueUpdateValidator.ValidateAsync(venueUpdateDTO);

            if (!validationResult.IsValid)
            {
                // Return a bad request response if validation fails
                
                return BadRequest(new ResponseDTO<object>(Array.Empty<object>() , "failure" , "Validation failed" , validationResult.Errors.Select(e=>e.ErrorMessage).ToList()));
            }

            try
            {
                // update the venue using the service layer
                var venueResponse = await venueService.UpdateVenue(venueUpdateDTO , VenueId);
                

                var venueResponseDTO = new VenueResponseDTO();
                mapper.Map(venueResponse, venueResponseDTO); // Map the response to the DTO

                if (venueResponse == null)
                {
                    // Return a success response with a message if no venue was found
                    

                    return Ok(new ResponseDTO<object>(Array.Empty<object>() , "success" , "No Id found"));
                }

                // Return a success response with the venue data
                

                return Ok(new ResponseDTO<VenueResponseDTO>(venueResponseDTO, "success", "Venue updates successfully"));
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error response in case of an exception
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<object>(Array.Empty<object>(), "failure", "An unexpected error occurred" , new List<string> { ex.Message}));
            }
        }

    }
}
