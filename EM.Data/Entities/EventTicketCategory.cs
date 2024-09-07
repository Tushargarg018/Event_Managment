using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class EventTicketCategory
    {
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Name of the ticket category
        /// </summary>
        [Column("name")]
		public required string Name { get; set; }

		/// <summary>
		/// event id for which ticket category is created
		/// </summary>
		[Column("event_id")]
        public int EventId { get; set; }

		/// <summary>
		/// Price of the ticket for the category
		/// </summary>
		[Column("price")]
		public int Price { get; set; }

		/// <summary>
		/// number of tickets available for the category
		/// </summary>
		[Column("capacity")]
		public int Capacity {  get; set; }

		/// <summary>
		/// creation date of the ticket category
		/// </summary>
		/// 
		[Column("created_on")]
		public DateTime CreatedOn { get; set; }

		/// <summary>
		/// modified date of the ticket category
		/// </summary>
		[Column("modified_on")]
		public DateTime ModifiedOn { get; set; }

		public Event Event { get; set; }
	}
}
