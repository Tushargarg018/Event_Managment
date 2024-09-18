﻿using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Repositories
{
    public interface IEventRepository
    {
        public Task<Event> AddEvent(Event eventToAdd);
        public Task<bool> EventExistsAsync(int eventId);
        public Task<bool> EventNotPublished(int eventId);
        public Event GetEventById(int eventId);
        public Task<Event> GetEventByIdAsync(int eventId);

        public Task<(List<Event> Events, int TotalCount)> GetEventsAsync(EventFilterDTO filter);

        public Task<Event> PublishEvent(Event e);
    }
}
