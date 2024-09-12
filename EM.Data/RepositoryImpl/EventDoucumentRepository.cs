using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.RepositoryImpl
{
    public class EventDoucumentRepository : IEventDocumentRepository
    {
        private readonly AppDbContext appDbContext;

        public EventDoucumentRepository(AppDbContext _appDbContext)
        {
            this.appDbContext = _appDbContext;
        }


        public async Task<bool> EventExistance(int EventId)
        {
            var eventid = await appDbContext.Events.Where(e => e.Id == EventId).FirstOrDefaultAsync();
            if (eventid == null)
            {
                return false;
            }
            return true;
        }

        public async Task<EventDocument> GetEventDocumentById(int DocId)
        {
            var doc = await appDbContext.EventDocuments.FindAsync(DocId);
            if (doc == null)
            {
                return null;
            }
            return doc;
        }

        public async Task<EventDocument> AddEventDocument(EventDocument eventDocument)
        { 
            await appDbContext.AddAsync(eventDocument);
            await appDbContext.SaveChangesAsync();
            return eventDocument;
        }

        public async Task<EventDocument> UpdateEventDocument(EventDocument eventDocument)
        {
            appDbContext.Update(eventDocument);
            await appDbContext.SaveChangesAsync();
            return eventDocument;
        }
    }
}
