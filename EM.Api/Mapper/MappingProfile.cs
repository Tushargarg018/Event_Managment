﻿using AutoMapper;
using EM.Business.BOs;
using EM.Core.DTOs.Objects;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOS.Response.Success;
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
            CreateMap<State, StateBo>();
            CreateMap<StateBo, StateDto>();

            CreateMap<City, CityBo>();
            CreateMap<CityBo, CityDto>();

            CreateMap<Organizer, OrganizerBo>();
            CreateMap<OrganizerBo, OrganizerDto>();

            CreateMap<LoginDto, LoginResponseBO>();
            CreateMap<LoginResponseBO, LoginResponseDTO>();

            CreateMap<Performer, PerformerBO>();
            CreateMap<PerformerDTO,  PerformerBO>();

            CreateMap<EventDTO, EventBO>();
            CreateMap<EventBO, Event>();
            CreateMap<Event, EventBO>();
            CreateMap<Performer, PerformerBO>();
            CreateMap<PerformerBO, PerformerResponseDTO>();
            CreateMap<Venue, VenueBO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name));
            CreateMap<VenueBO, VenueResponseDTO>();

            CreateMap<EventDocument, EventDocumentBO>();
            CreateMap<EventDocumentBO, EventDocumentResponseDTO>();

            CreateMap<EventTicketCategory, EventPriceCategoryBO>();
            CreateMap<EventPriceCategoryBO, EventPriceCategoryResponseDTO>();
        }
    }
}
