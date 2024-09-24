using EM.Core.DTOs.Request;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repository
{
    public interface IVenueRepository
    {
        public Task<Venue> AddVenue(Venue venue);

        public Task<IEnumerable<Venue>> GetVenueList();

        public Task<Venue> GetVenue(int VenueId);

        public Task<Venue> UpdateVenue(VenueUpdateDTO venueUpdateDTO, int VenueId);

        public Task<Venue> GetVenueById(int VenueId);   
        public Task<bool> VenueExistsAsync(int venueId);
        public Task<int> GetVenueCapacityByIdAsync(int venueId);

        public Task<bool> VenueNameExistsAsync(string venueName);

    }
}
