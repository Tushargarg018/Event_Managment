using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public int OrganizerId {  get; set; }
        public Organizer Organizer { get; set; }
        public int PerformerId { get; set; }
        public Performer Performer { get; set; }
        public int VenueId { get; set; }
        public Venue Venue { get; set; }
        public StatusEnum status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }

        public virtual ICollection<EventOffer> EventOffers { get; set; }
        public virtual ICollection<EventDocument> EventDocuments { get; set; }
        public virtual ICollection<EventTicketCategory> EventTicketCategories { get; set; }

    }
}
