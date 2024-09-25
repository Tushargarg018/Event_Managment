using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EM.Core.DTOs.Request;

namespace EM.Core.DTOs.Response.Success
{
    public class EventListResponseDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("base_price")]
        public decimal BasePrice { get; set; }

        [JsonPropertyName("organizer_id")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("performer_id")]
        public int PerformerId { get; set; }

        [JsonPropertyName("venue_id")]
        public int VenueId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }

        [JsonPropertyName("start_date_time")]
        public string StartDateTime { get; set; }

        [JsonPropertyName("end_date_time")]
        public string EndDateTime { get; set; }

        [JsonPropertyName("performer")]
        public PerformerResponseDTO? Performer { get; set; }

        [JsonPropertyName("venue")]

        public VenueResponseDTO? Venue { get; set; }

        [JsonPropertyName("event_documents")]
        public List <EventDocumentResponseDTO> EventDocument { get; set; }

        [JsonPropertyName("event_price_category")]
        public List<EventPriceCategoryResponseDTO> EventPriceCategories { get; set; }

        [JsonPropertyName("event_offers")]
        public List <OfferResponseDTO> Offers { get; set; }

        [JsonPropertyName("tax_details")]
        public required TaxDetailDTO TaxDetail { get; set; }

    }
}
