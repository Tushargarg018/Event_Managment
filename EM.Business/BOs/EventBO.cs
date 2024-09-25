using EM.Core.Enums;
using EM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Business.BOs
{
    public class EventBO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string Currency { get; set; }
        public int OrganizerId { get; set; }
        public int PerformerId { get; set; }
        public int VenueId { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }    
        public VenueBO? Venue { get; set; }
        public PerformerBO? Performer { get; set; }

        public List <EventDocumentBO> EventDocument { get; set; }

        public List<EventPriceCategoryBO> EventPriceCategory { get; set; }

        public List<OfferBO> Offer { get; set; }
    }
}
