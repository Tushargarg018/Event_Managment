using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.RepositoryImpl
{
    public class PerformerRepository : IPerformerRepository
    {
        private readonly AppDbContext context;

        public PerformerRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public async Task<Performer> AddPerformer(Performer performer)
        {
            using (var transaction = await context.Database.BeginTransactionAsync()) {
                try
                {
                    await context.Performers.AddAsync(performer);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return performer;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            
        } 


        //public IEnumerable<Performer> GetPerformersUsingOrganizer(int organizerId)
        //{
        //    var performer = context.Performers.Where(p=>p.OrganizerId == organizerId).ToList();
        //    return performer;  
        //}

        public async Task<bool> PerformerExistsAsync(int performerId)
        {
            return await context.Performers.AnyAsync(p => p.Id == performerId);
        }

        public async Task<Performer> GetPerformerById(int id)
        {
            return await context.Performers.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Performer> UpdatePerformer(string bio, string name, string profile_pic, int performer_id)
        {
            var performer = await context.Performers.FirstOrDefaultAsync(p => p.Id == performer_id);
            if (performer == null)
            {
                throw new Exception("Performer does not exist");
            }
            performer.Bio = bio;
            performer.Name = name;
            performer.Profile = profile_pic;
            await context.SaveChangesAsync();
            return performer;
        }   

        public async Task<string> GetPerformerProfilePath(int performerId)
        {
            var performer = await context.Performers.FirstOrDefaultAsync(p => p.Id == performerId);
            return performer.Profile;
        }

        public async Task<IEnumerable<Performer>> GetPerformers()
        {
            return await context.Performers.ToListAsync(); 
        }

        public async Task UpdatePerformerImage(string fileName, int id)
        {
            Performer performer = await context.Performers.FirstOrDefaultAsync(p => p.Id == id);
            if (performer == null)
            {
                throw new Exception("Performer Does not Exist");
            }
            performer.Profile = fileName;
            await context.SaveChangesAsync();
        }
    }
}
