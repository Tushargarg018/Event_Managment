using AutoMapper;
using EM.Business.BOs;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.ServiceImpl
{
    public class EventDocumentService : IEventDocumentService
    {
        private readonly IEventDocumentRepository eventDocumentrepository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public EventDocumentService(IEventDocumentRepository repository , IMapper mapper  , IWebHostEnvironment environment)
        {
            this.eventDocumentrepository = repository;
            this.mapper = mapper;
            this.environment = environment;
        }


        public async Task<EventDocumentBO> AddEventDocuments(EventDocumentRequestDTO eventRequestDocument, int eventId , string file_path)
        {

            var eventid = await eventDocumentrepository.EventExistance(eventId);
            if (eventid == null)
            {
                return null;
            }

            EventDocument eventDocument = new EventDocument
            {
                Type = eventRequestDocument.Type,
                EventId = eventId,
                Title = eventRequestDocument.Title,
                FilePath = file_path,
                CreatedOn = DateTime.UtcNow,
                ModifiedOn = DateTime.UtcNow
            };

            EventDocument newEventDocument = await eventDocumentrepository.AddEventDocument(eventDocument);
            EventDocumentBO eventDocumentBO = new EventDocumentBO();

            mapper.Map(newEventDocument , eventDocumentBO);
            eventDocumentBO.Type = newEventDocument.Type.ToString();
            return eventDocumentBO;


        }

        public async Task<EventDocumentBO> UpdateEventDocuments(EventDocumentRequestDTO eventRequestDocument, int eventId, string file_path)
        {

            var eventid = await eventDocumentrepository.EventExistance(eventId);
            if (eventid == null)
            {
                return null;
            }

            var existingDocument = await eventDocumentrepository.GetEventDocumentById((int)eventRequestDocument.Id);
            if (existingDocument == null)
            {
                throw new Exception("Document doesnot exist to update");
            }

            if (!string.IsNullOrEmpty(existingDocument.FilePath))
            {
                var oldImagePath = Path.Combine(environment.ContentRootPath, existingDocument.FilePath.Replace("/", "\\"));
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }

            existingDocument.Type = eventRequestDocument.Type;
            existingDocument.Title = eventRequestDocument.Title;
            existingDocument.FilePath = file_path;
            existingDocument.ModifiedOn = DateTime.UtcNow;


            EventDocument newEventDocument = await eventDocumentrepository.UpdateEventDocument(existingDocument);
            EventDocumentBO eventDocumentBO = new EventDocumentBO();

            mapper.Map(newEventDocument, eventDocumentBO);
            eventDocumentBO.Type = newEventDocument.Type.ToString();
            return eventDocumentBO;
        }
    }
}


