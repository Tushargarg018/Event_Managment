using AutoMapper;
using EM.Business.BOs;
using EM.Business.Exceptions;
using EM.Business.Services;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using EM.Data.Repositories;
using EM.Data.RepositoryImpl;
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
        private readonly IEventRepository _eventRepository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public EventDocumentService(IEventDocumentRepository repository , IMapper mapper  , IWebHostEnvironment environment, IEventRepository eventRepository)
        {
            this.eventDocumentrepository = repository;
            _eventRepository = eventRepository;
            this.mapper = mapper;
            this.environment = environment;
        }

        /// <summary>
        /// Add document in event
        /// </summary>
        /// <param name="eventRequestDocument"></param>
        /// <param name="eventId"></param>
        /// <param name="file_path"></param>
        /// <returns></returns>
        public async Task<EventDocumentBO> AddEventDocuments(EventDocumentRequestDTO eventRequestDocument, int eventId , string file_path)
        {

            var eventid = await _eventRepository.GetEventByIdAsync(eventId) ?? throw new NotFoundException("Event");

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
            eventDocumentBO.Type = newEventDocument.Type;
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
            eventDocumentBO.Type = newEventDocument.Type;
            return eventDocumentBO;
        }
    }
}


