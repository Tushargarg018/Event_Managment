using EM.Business.BOs;
using EM.Core.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IEventDocumentService
    {
        public Task<EventDocumentBO> AddEventDocuments(EventDocumentRequestDTO eventDocument , int EventId , string file_path);

        public Task<EventDocumentBO> UpdateEventDocuments(EventDocumentRequestDTO eventDocument , int EventId , string file_path);
    }
}
