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
using System.Text.Json;
using System.Threading.Tasks;

namespace EM.Api.Mapper
{
    public class MappingProfile : Profile
    {
        //private readonly string _baseurl;
        public MappingProfile()
        {
            //IConfiguration configuration
            //_baseurl = configuration["Appsettings:BaseUrl"];

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
                .ForMember(dest => dest.EventDocument, opt => opt.MapFrom(src => src.EventDocuments))
                .ForMember(dest => dest.EventPriceCategory, opt => opt.MapFrom(src => src.EventTicketCategories))
                .ForMember(dest => dest.Offer, opt => opt.MapFrom(src => src.EventOffers));

            CreateMap<EventBO, EventResponseDTO>();

            CreateMap<Currency, CurrencyDTO>();

            CreateMap<Performer, PerformerBO>();
            CreateMap<PerformerBO, PerformerResponseDTO>();
            CreateMap<Venue, VenueBO>()
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.State.Name))
                .ForMember(dest=>dest.TypeId, opt=>opt.MapFrom(src=>(int)src.Type));
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
            //CreateMap<TaxDetailBO, TaxDetailDTO>();
            CreateMap<TaxConfiguration, TaxDetailBO>();


            //CreateMap<TaxDetailBO, TaxDetailDTO>()
            //.ForMember(dest => dest.TaxDetails, opt => opt.MapFrom(src => src.TaxDetails.RootElement.ToString()));

            CreateMap<TaxDetailBO, TaxDetailDTO>()
            .ForMember(dest => dest.TaxDetails,
                       opt => opt.MapFrom(src => src.TaxDetails != null ? src.TaxDetails.RootElement.ToString() : null)); // Handle null check

            //CreateMap<TaxDetailDTO, TaxDetailBO>()
            //    .ForMember(dest => dest.TaxDetails, opt => opt.MapFrom(src => JsonDocument.Parse(src.TaxDetails)));
        }
    }
}
