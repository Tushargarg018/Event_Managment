using EM.Business.BOs;
using EM.Core.DTOs.Request;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public interface IEventPriceCategoryService
    {
        public Task<EventPriceCategoryBO> AddorUpdateEventPriceCategory(EventPriceCategoryRequestDTO eventPriceCategoryRequestDTO);

    }
}
