using EM.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EM.Data.Entities
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// title of the event
        /// </summary>
        [Column("title")]
		public string Title { get; set; }

        /// <summary>
        /// Description of the event
        /// </summary>
        [Column("description")]
		public  string Description { get; set; }
        /// <summary>
        /// base price of the event
        /// </summary>
        [Column("base_price")]
		public decimal BasePrice { get; set; }

        /// <summary>
        /// Organizer Id
        /// </summary>        
        [Column("organizer_id")]
		public int OrganizerId {  get; set; }

        /// <summary>
        /// Performer Id
        /// </summary>
        [Column("performer_id")]
		public int PerformerId { get; set; }

        /// <summary>
        /// Venue Id
        /// </summary>
        /// 
        [Column("venue_id")]
		public int VenueId { get; set; }

        /// <summary>
        /// Status of the event
        /// </summary>
        [Column("status")]
		public StatusEnum Status { get; set; }

		/// <summary>
		/// Start date and time of the event
		/// </summary>
		[Column("start_datetime")]
		/// </summary>
		public DateTime StartDatetime { get; set; }

        /// <summary>
        /// End date and time of the event
        /// </summary>
        [Column("end_datetime")]
		public DateTime EndDatetime { get; set; }

		/// <summary>
		/// Date and time when the event was created
		/// </summary>        
		[Column("created_on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date and time when the event was last modified
        /// </summary>
        /// 
        [Column("modified_on")]
		public DateTime ModifiedOn { get; set; }
		public Organizer Organizer { get; set; }
		public Performer Performer { get; set; }
		public Venue Venue { get; set; }
		public virtual ICollection<EventOffer>? EventOffers { get; set; }
        public virtual ICollection<EventDocument>? EventDocuments { get; set; }
        public virtual ICollection<EventTicketCategory>? EventTicketCategories { get; set; }

    }
}
