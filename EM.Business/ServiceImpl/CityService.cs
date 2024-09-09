using AutoMapper;
using EM.Business.BOs;
using EM.Data;
using EM.Data.Entities;
using EM.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Business.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;

        public CityService(ICityRepository cityRepository , IMapper mapper)
        {
            this.cityRepository = cityRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CityBo>> GetCities(int StateId)
        {
            var StateExits = await cityRepository.StateExist(StateId);
            
            if (!StateExits)
            {
                return null;
            }
            var cities =  await cityRepository.GetCityList(StateId);
            var cityBo = new List<CityBo>();
            mapper.Map(cities, cityBo);
            return cityBo;
        }
    }
}
