using EM.Business.BOs;
using EM.Core.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IEventService
    {
        public EventBO AddEvent(EventDTO eventDto, int organizerId);
    }
}
