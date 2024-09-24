using EM.Core.DTOs.Request;
using EM.Core.Enums;
using EM.Data.Entities;
using EM.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.RepositoryImpl
{
    public class VenueRepository : IVenueRepository
    {
        private readonly AppDbContext appDbContext;
        public VenueRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;  
        }
        public async Task<Venue> AddVenue(Venue venue)
        {
            await appDbContext.AddAsync(venue);
            await appDbContext.SaveChangesAsync();
			var newVenue = await appDbContext.Venues
		                        .Include(v => v.City)
		                        .Include(v => v.State)
		                        .FirstOrDefaultAsync(v => v.Id == venue.Id);
			return newVenue;   
        }

        public async Task<IEnumerable<Venue>> GetVenueList()
        {
            var venues = await appDbContext.Venues
                            .Include(v => v.City)
                            .Include(v => v.State)
                            .ToListAsync();
            return venues;
        }

        public async Task<Venue> GetVenue(int venueId)
        {
            var venue = await appDbContext.Venues.Where(v => v.Id == venueId)
				 .Include(v => v.City)
				 .Include(v => v.State)
				.FirstOrDefaultAsync();
            return venue;
        }

        public async Task<Venue> UpdateVenue(VenueUpdateDTO VenueUpdatetDTO, int VenueId)
        {
            var venue = await appDbContext.Venues.Where(v => v.Id == VenueId).FirstOrDefaultAsync();
            if (venue == null)
            {
                return null;
            }
            venue.Name = VenueUpdatetDTO.Name;
            venue.MaxCapacity = VenueUpdatetDTO.MaxCapacity;
            venue.AddressLine1 = VenueUpdatetDTO.AddressLine1;
            venue.AddressLine2 = VenueUpdatetDTO.AddressLine2;
            venue.ZipCode = VenueUpdatetDTO.ZipCode;
            venue.CityId = VenueUpdatetDTO.City;
            venue.StateId = VenueUpdatetDTO.State;
            venue.Description = VenueUpdatetDTO.Description;
            venue.ModifiedOn = DateTime.UtcNow;

            await appDbContext.SaveChangesAsync();
            var newVenue = await appDbContext.Venues
                                .Include(v => v.City)
                                .Include(v => v.State)
                                .FirstOrDefaultAsync(v => v.Id == VenueId);
            return newVenue;
        }

        public async Task<Venue> GetVenueById(int venueId)
        {
            return await appDbContext.Venues.FirstOrDefaultAsync(v => v.Id == venueId);
        }

        public async Task<bool> VenueExistsAsync(int venueId)
        {
            return await appDbContext.Venues.AnyAsync(v=>v.Id == venueId);
        }

        public async Task<int> GetVenueCapacityByIdAsync(int venueId)
        {
            var venue = await appDbContext.Venues.FindAsync(venueId);
            return venue?.MaxCapacity ?? 0;
        }

        public async Task<bool> VenueNameExistsAsync(string venueName)
        {
            return await appDbContext.Venues.AnyAsync(v => v.Name.ToLower()==venueName.ToLower());
        }
    }

}

 

