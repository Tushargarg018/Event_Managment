using AutoMapper;
using EM.Business.BOs;
<<<<<<< HEAD
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOS.Response.Success;
using EM.Data.Entities;
=======
using EM.Core.DTOs.Objects;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> cad3ae3dc4920a67dd9ff6fbb36a72f79aa95e13

namespace EM.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
<<<<<<< HEAD
            
            CreateMap<State, StateBo>();
            CreateMap<StateBo, StateDto>();

            CreateMap<City, CityBo>();
            CreateMap<CityBo, CityDto>();
=======
            CreateMap<Organizer, OrganizerBo>();
            CreateMap<OrganizerBo, OrganizerDto>();
            CreateMap<LoginDto, LoginResponseBO>();
            CreateMap<LoginResponseBO, LoginResponseDTO>();
            CreateMap<Performer, PerformerBO>();
            CreateMap<PerformerDTO,  PerformerBO>();
>>>>>>> cad3ae3dc4920a67dd9ff6fbb36a72f79aa95e13
        }
    }
}
