using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class EventOffer
    {
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// event id for which offer is created
        /// </summary>
        [Column("event_id")]
		public int EventId { get; set; }

        /// <summary>
        /// type of offer
        /// </summary>
        [Column("type")]
		public OfferTypeEnum Type { get; set; }

        /// <summary>
        /// discount percentage on the offer
        /// </summary>
        [Column("discount")]
		public decimal Discount { get; set; }

        /// <summary>
        /// total offers available or threshold for the offer
        /// </summary>
        [Column("total_offers")]
		public int TotalOffers { get; set; }

        /// <summary>
        /// if type is group then group size
        /// </summary>
        [Column("group_size")]
		public int GroupSize { get; set; }

        /// <summary>
        /// status of the offer
        /// </summary>
        [Column("status")]
		public int Status { get; set; }

        /// <summary>
        /// created date of the offer
        /// </summary>
        /// 
        [Column("created_on")]
		public DateTime CreatedOn{ get; set; }

        /// <summary>
        /// modified date of the offer
        /// </summary>
        [Column("modified_on")]
		public DateTime ModifiedOn { get; set; }

		public Event Event { get; set; }
	}
}
