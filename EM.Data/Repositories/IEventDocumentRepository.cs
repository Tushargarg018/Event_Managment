using EM.Core.DTOs.Request;
using EM.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IEventDocumentRepository
    {
        public Task<EventDocument> AddEventDocument(EventDocument eventDocument);
        public Task<EventDocument> UpdateEventDocument(EventDocument eventDocument);

        public Task<EventDocument> GetEventDocumentById(int id);

        public Task<bool> EventExistance(int EventId);
        
    }
}
