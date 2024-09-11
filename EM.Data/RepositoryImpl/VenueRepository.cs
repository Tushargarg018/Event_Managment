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
<<<<<<< HEAD
            this.appDbContext = appDbContext;
        }

        public async Task<Venue> AddVenue(Venue venue)
        {

            await appDbContext.AddAsync(venue);
            await appDbContext.SaveChangesAsync();

            return venue;
=======
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
>>>>>>> a35f1aafa7855c433e8d8255875ec6b99b2bab67
        }

        public async Task<IEnumerable<Venue>> GetVenueList(int organizerId)
        {
<<<<<<< HEAD
            return await appDbContext.Venues.Where(o => o.OrganizerId == organizerId).ToListAsync();
        }

        public async Task<Venue> GetVenue(int VenueId)
        {
            var venue = await appDbContext.Venues.Where(v => v.Id == VenueId).FirstOrDefaultAsync();
            if (venue == null)
            {
                return null;
            }
=======
			var venues = await appDbContext.Venues
	                        .Where(v => v.OrganizerId == organizerId)
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
>>>>>>> a35f1aafa7855c433e8d8255875ec6b99b2bab67
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
<<<<<<< HEAD
            venue.City = VenueUpdatetDTO.City;
            venue.State = VenueUpdatetDTO.State;
=======
            venue.CityId = VenueUpdatetDTO.City;
            venue.StateId = VenueUpdatetDTO.State;
>>>>>>> a35f1aafa7855c433e8d8255875ec6b99b2bab67
            venue.Description = VenueUpdatetDTO.Description;
            venue.ModifiedOn = DateTime.UtcNow;

            await appDbContext.SaveChangesAsync();
            return venue;

        }

<<<<<<< HEAD
        public async Task<bool> VenueExistsAsync(int venueId)
        {
            return await appDbContext.Venues.AnyAsync(v=>v.Id == venueId);
        }
    }

}
=======
    }

}
>>>>>>> a35f1aafa7855c433e8d8255875ec6b99b2bab67
