﻿using AutoMapper;
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

        public async Task<VenueBO> AddVenue(VenueRequestDTO venueRequestDTO)
        {

            Venue venue = new Venue
            {
                Name = venueRequestDTO.Name,           
                Type = venueRequestDTO.Type,
                MaxCapacity = venueRequestDTO.MaxCapacity,
                AddressLine1 = venueRequestDTO.AddressLine1,
                AddressLine2 = venueRequestDTO.AddressLine2,
                ZipCode = venueRequestDTO.ZipCode,
                CityId = venueRequestDTO.City,
                StateId = venueRequestDTO.State,
                CountryId = venueRequestDTO.CountryId,
                Description = venueRequestDTO.Description,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            var newVenue = await venueRepository.AddVenue(venue);
			VenueBO venueBo =  mapper.Map<VenueBO>(newVenue);
            venueBo.TypeId = (int)newVenue.Type;
            return venueBo;
        }

        public async Task<IEnumerable<VenueBO>> GetAllVenue()
        {
            var venues = await venueRepository.GetVenueList();
            return mapper.Map<List<VenueBO>>(venues);
        }

        public async Task<VenueBO> GetVenue(int venueId)
        {
            var venues = await venueRepository.GetVenue(venueId);
            return mapper.Map<VenueBO>(venues);
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
