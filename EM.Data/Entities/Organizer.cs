using EM.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EM.Data.Entities
{
    public class Organizer
    {
        [Column]
        public int Id { get; set; }

        /// <summary>
        /// name of the organizer
        /// </summary>
        [Column("name")]
		public required string Name { get; set; }

        /// <summary>
        /// Email of the organizer
        /// </summary>
        [Column("email")]
		public required string Email { get; set; }

        /// <summary>
        /// Password of the organizer
        /// </summary>
        [Column("password")]
		public required string Password { get; set; }

        /// <summary>
        /// creation date of the organizer
        /// </summary>
        [Column("created_on")]
		public DateTime  CreatedOn { get; set; }

        /// <summary>
        /// modified date of the organizer
        /// </summary>
        [Column("modified_on")]
		public DateTime ModifiedOn { get; set; }

		public UserStatus Status { get; set; }
		public virtual ICollection<Performer> Performers { get; set; }
        public virtual ICollection<Venue> Venues { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
