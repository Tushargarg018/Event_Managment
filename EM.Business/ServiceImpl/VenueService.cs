using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository venueRepository;
        private readonly IMapper mapper;
        public VenueService(IVenueRepository venueRepository, IMapper mapper)
        {
            this.venueRepository = venueRepository;
            this.mapper = mapper;
        }

        public async Task<VenueBO> AddVenue(VenueRequestDTO venueRequestDTO , int organizerId)
        {

            Venue venue = new Venue
            {
                Name = venueRequestDTO.Name,           
                Type = venueRequestDTO.Type,
                MaxCapacity = venueRequestDTO.MaxCapacity,
                AddressLine1 = venueRequestDTO.AddressLine1,
                AddressLine2 = venueRequestDTO.AddressLine2,
                ZipCode = venueRequestDTO.ZipCode,
                City = venueRequestDTO.City,
                State = venueRequestDTO.State,
                Country = venueRequestDTO.Country,
                OrganizerId = organizerId,  
                Description = venueRequestDTO.Description,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            var newVenue = await venueRepository.AddVenue(venue);

            var newVenueBO = new VenueBO();

            mapper.Map(newVenue , newVenueBO);

            return newVenueBO;

        }

        public async Task<IEnumerable<VenueBO>> GetAllVenue(int organizerId)
        {
            var venues = await venueRepository.GetVenueList(organizerId);
            var newVenueBO = new List<VenueBO>();
            mapper.Map(venues , newVenueBO);
            return newVenueBO;
        }

        public async Task<VenueBO> GetVenue(int VenueId)
        {
            var venues = await venueRepository.GetVenue(VenueId);
            if(venues == null)
            {
                return null;
            }
            var newVenueBO = new VenueBO();
            mapper.Map(venues, newVenueBO);
            return newVenueBO;
        }

        public async Task<VenueBO> UpdateVenue(VenueUpdateDTO venueUpdateDTO, int VenueId)
        {

            var newVenue = await venueRepository.UpdateVenue(venueUpdateDTO , VenueId);
            if(newVenue == null)
            {
                return null;
            }

            var newVenueBO = new VenueBO();

            mapper.Map(newVenue, newVenueBO);

            return newVenueBO;
        }
    }
}
