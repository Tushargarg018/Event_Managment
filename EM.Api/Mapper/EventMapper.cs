using AutoMapper;
using EM.Core.DTOs.Response.Success;
using EM.Core.DTOs.Response;
using EM.Core.Helpers;
using EM.Data.Entities;
using EM.Business.BOs;

public class EventMapper
{
    private readonly IMapper _mapper;

    public EventMapper(IMapper mapper)
    {
        _mapper = mapper;
    }
    /// <summary>
    /// Maps EventBO to EventListResponseDTO
    /// </summary>
    /// <param name="events"></param>
    /// <returns></returns>
    public List<EventListResponseDTO> MapToEventListResponse(List<EventBO> events)
    {
        return events.Select(e => new EventListResponseDTO
        {
            Id = e.Id,
            Title = e.Title,
            Description = e.Description,
            BasePrice = e.BasePrice,
            Status = e.Status.ToString(),
            OrganizerId = e.OrganizerId,
            PerformerId = e.PerformerId,
            VenueId = e.VenueId,
            CreatedOn = TimeConversionHelper.ConvertFromUTCAndTruncate(e.CreatedOn),
            ModifiedOn = TimeConversionHelper.ConvertFromUTCAndTruncate(e.ModifiedOn),
            StartDateTime = TimeConversionHelper.ToCustomDateTimeString(e.StartDateTime),
            EndDateTime = TimeConversionHelper.ToCustomDateTimeString(e.EndDateTime),
            Performer = _mapper.Map<PerformerResponseDTO>(e.Performer),
            Venue = _mapper.Map<VenueResponseDTO>(e.Venue),
            EventDocument = e.EventDocument.Select(ed => _mapper.Map<EventDocumentResponseDTO>(ed)).ToList(),
            EventPriceCategories = e.EventPriceCategory.Select(epc => _mapper.Map<EventPriceCategoryResponseDTO>(epc)).ToList(),
            Offers = e.Offer.Select(o => _mapper.Map<OfferResponseDTO>(o)).ToList()
        }).ToList();
    }
    /// <summary>
    /// Maps Event to EventListResponseDTO
    /// </summary>
    /// <param name="_event"></param>
    /// <returns></returns>
    public EventListResponseDTO MapToEventResponse(EventBO _event)
    {
        return new EventListResponseDTO
        {
            Id = _event.Id,
            Title = _event.Title,
            Description = _event.Description,
            BasePrice = _event.BasePrice,
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
            Offers = _event.Offer.Select(o => _mapper.Map<OfferResponseDTO>(o)).ToList()
        };
    }
}
