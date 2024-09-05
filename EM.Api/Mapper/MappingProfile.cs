using AutoMapper;
using EM.Business.BOs;
using EM.Core.DTOs.Objects;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response.Success;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Organizer, OrganizerBo>();
            CreateMap<OrganizerBo, OrganizerDto>();
            CreateMap<LoginDto, LoginResponseBO>();
            CreateMap<LoginResponseBO, LoginResponseDTO>();
        }
    }
}
