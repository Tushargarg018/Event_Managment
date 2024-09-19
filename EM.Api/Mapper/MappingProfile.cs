﻿using AutoMapper;
using EM.Business.BOs;
using EM.Core.DTOs.Objects;
using EM.Core.DTOs.Request;
using EM.Core.DTOs.Response;
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOS.Response.Success;
using EM.Core.Helpers;
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

            
            CreateMap<PerformerDTO,  PerformerBO>();

            CreateMap<EventDTO, EventBO>();
            CreateMap<EventBO, Event>();
            CreateMap<Event, EventBO>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.CreatedOn)))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.ModifiedOn)))
                .ForMember(dest=>dest.StartDateTime, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.StartDatetime)))
                .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.EndDatetime)))
                .ForMember(dest => dest.EventDocument, opt => opt.MapFrom(src => src.EventDocuments))
                .ForMember(dest => dest.EventPriceCategory, opt => opt.MapFrom(src => src.EventTicketCategories))
                .ForMember(dest => dest.Offer, opt => opt.MapFrom(src => src.EventOffers));

            CreateMap<EventBO, EventResponseDTO>();
                //.ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.CreatedOn)))
                //.ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.ModifiedOn)));
                
            CreateMap<Performer, PerformerBO>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.CreatedOn)))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.ModifiedOn)));
            CreateMap<PerformerBO, PerformerResponseDTO>();
            CreateMap<Venue, VenueBO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name))
                .ForMember(dest => dest.Created_on, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.CreatedOn)))
                .ForMember(dest => dest.ModifiedOn, opt => opt.MapFrom(src => TimeConversionHelper.ConvertFromUTCAndTruncate(src.ModifiedOn)));
            CreateMap<VenueBO, VenueResponseDTO>();
            CreateMap<EventDocument, EventDocumentBO>();
            CreateMap<EventDocumentBO, EventDocumentResponseDTO>();

            CreateMap<OfferDTO, EventOffer>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.TotalOffers, opt => opt.MapFrom(src => src.Quantity));
            CreateMap<EventOffer, OfferBO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.Type));
            CreateMap<EventTicketCategory, EventPriceCategoryBO>();
            CreateMap<EventPriceCategoryBO, EventPriceCategoryResponseDTO>();
            CreateMap<OfferBO, OfferResponseDTO>();
        }
    }
}
