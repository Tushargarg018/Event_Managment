using AutoMapper;
using EM.Business.BOs;
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOS.Response.Success;
using EM.Data.Entities;

namespace EM.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<State, StateBo>();
            CreateMap<StateBo, StateDto>();

            CreateMap<City, CityBo>();
            CreateMap<CityBo, CityDto>();
        }
    }
}
