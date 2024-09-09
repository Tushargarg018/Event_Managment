﻿using EM.Business.BOs;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IVenueService
    {
        public Task<VenueBO> AddVenue(VenueRequestDTO venueRequestDTO  , int organizerId);
        public Task<IEnumerable<VenueBO>> GetAllVenue(int organizerId);
        public Task<VenueBO> GetVenue(int VenueId);

        public Task<VenueBO> UpdateVenue(VenueUpdateDTO venueUpdateDTO ,int VenueId);
    }
}
