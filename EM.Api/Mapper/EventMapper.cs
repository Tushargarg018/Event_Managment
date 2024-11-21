using AutoMapper;
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOs.Response;
using EM.Core.Helpers;
using EM.Data.Entities;
using EM.Business.BOs;

public class EventMapper
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;

    public EventMapper(IMapper mapper, IConfiguration config)
    {
        _mapper = mapper;
        _config = config;
    }
    private PerformerResponseDTO setPerformerUrl(PerformerBO performerBo)
    {
        var performerResponseDto = _mapper.Map<PerformerResponseDTO>(performerBo);
        performerResponseDto.Profile = _config["Appsettings:BaseUrl"] +'/'+ performerResponseDto.Profile;
        return performerResponseDto;
    }
    private EventDocumentResponseDTO setEventDocumentUrl(EventDocumentBO eventDocumentBo)
    {
        var eventDocumentDto = _mapper.Map<EventDocumentResponseDTO>(eventDocumentBo);
        eventDocumentDto.FilePath = _config["Appsettings:BaseUrl"] + '/' + eventDocumentDto.FilePath;
        return eventDocumentDto;
    }
    /// <summary>
    /// Maps EventBO to EventListResponseDTO
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    public List<EventListResponseDTO> MapToEventListResponse(List<EventBO> events)
    {

        var eventListResponse =  events.Select(e => new EventListResponseDTO
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            BasePrice = e.BasePrice,
            Currency = e.Currency,
            Status = e.Status.ToString(),
            OrganizerId = e.OrganizerId,
            PerformerId = e.PerformerId,
            VenueId = e.VenueId,
            CreatedOn = e.CreatedOn,
            ModifiedOn = e.ModifiedOn,
            StartDateTime = TimeConversionHelper.ToCustomDateTimeString(e.StartDateTime),
            EndDateTime = TimeConversionHelper.ToCustomDateTimeString(e.EndDateTime),
            Performer = setPerformerUrl(e.Performer),
            Venue = _mapper.Map<VenueResponseDTO>(e.Venue),
            EventDocument = e.EventDocument.Select(ed => setEventDocumentUrl(ed)).ToList(),
            EventPriceCategories = e.EventPriceCategory.Select(epc => _mapper.Map<EventPriceCategoryResponseDTO>(epc)).ToList(),
            Offers = e.Offer.Select(o => _mapper.Map<OfferResponseDTO>(o)).ToList(),
            TaxDetail = e.TaxDetail != null ? e.TaxDetail : null
        }).ToList();
        return eventListResponse;
    }
    /// <summary>
    /// Maps Event to EventListResponseDTO
    /// </summary>
    /// <param name="_event"></param>
    /// <returns></returns>
    public EventListResponseDTO MapToEventResponse(EventBO _event)
    {

        //var taxDetailDto = _event.TaxDetail != null
        //? new TaxDetailDTO { TaxDetails = _event.TaxDetail ?.RootElement.ToString() }
        //: null;
        var eventResponse = new EventListResponseDTO
        {
            Id = _event.Id,
            Title = _event.Title,
            Description = _event.Description,
            BasePrice = _event.BasePrice,
            Currency = _event.Currency,
            Status = _event.Status.ToString(),
            OrganizerId = _event.OrganizerId,
            PerformerId = _event.PerformerId,
            VenueId = _event.VenueId,
            CreatedOn = _event.CreatedOn,
            ModifiedOn = _event.ModifiedOn,
            StartDateTime = TimeConversionHelper.ToCustomDateTimeString(_event.StartDateTime),
            EndDateTime = TimeConversionHelper.ToCustomDateTimeString(_event.EndDateTime),
            Performer = _mapper.Map<PerformerResponseDTO>(_event.Performer),
            Venue = _mapper.Map<VenueResponseDTO>(_event.Venue),
            EventDocument = _event.EventDocument.Select(ed => _mapper.Map<EventDocumentResponseDTO>(ed)).ToList(),
            EventPriceCategories = _event.EventPriceCategory.Select(epc => _mapper.Map<EventPriceCategoryResponseDTO>(epc)).ToList(),
            Offers = _event.Offer.Select(o => _mapper.Map<OfferResponseDTO>(o)).ToList(),
            Flag = _event.Flag,
            TaxDetail = _event.TaxDetail != null ? _event.TaxDetail : null

        };
        eventResponse.Performer.Profile = _config["Appsettings:BaseUrl"] + '/' + eventResponse.Performer.Profile;
        foreach(var ed in eventResponse.EventDocument)
        {
            ed.FilePath = _config["Appsettings:BaseUrl"] + '/' + ed.FilePath;
        }
        return eventResponse;
    }
}
