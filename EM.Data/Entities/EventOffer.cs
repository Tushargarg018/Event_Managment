using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class EventOffer
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public OfferTypeEnum Type { get; set; }
        public decimal Discount { get; set; }
        public int TotalOffers { get; set; }
        public int GroupSize { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn{ get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
