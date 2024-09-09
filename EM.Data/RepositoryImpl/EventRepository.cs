﻿using EM.Data.Entities;
using EM.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.RepositoryImpl
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext context;

        public EventRepository(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public Event AddEvent(Event eventToAdd)
        {
            var lastId = context.Events
                              .OrderByDescending(e => e.Id)
                              .Select(e => e.Id)
                              .FirstOrDefault();
            var eventId = lastId + 1;
            eventToAdd.Id = eventId;
            context.Events.Add(eventToAdd);
            context.SaveChangesAsync();
            return eventToAdd;
        }
    }
}
