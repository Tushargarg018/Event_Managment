using EM.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EM.Data.Entities
{

    public class Venue
    {
        [Column("id")]
        public int Id  { get; set; }

        /// <summary>
        /// name of the venue
        /// </summary>
        [Column("name")]
		public required string Name { get; set; }

        /// <summary>
        /// type of the venue
        /// </summary>
        [Column("type")]
		public VenueTypeEnum Type {  get; set; }

        /// <summary>
        /// Maximum capacity of the venue
        /// </summary>
        [Column("max_capacity")]
		public int MaxCapacity { get; set; }

        /// <summary>
        /// address line 1 of the venue
        /// </summary>
        [Column("address_line1")]
		public required string AddressLine1 { get; set; }

        /// <summary>
        /// address line 2 of the venue
        /// </summary>
        [Column("address_line2")]
		public string? AddressLine2 { get; set; }

        /// <summary>
        /// Pin code of the venue
        /// </summary>
        /// 
        [Column("zip_code")]
		public int ZipCode { get; set; }

        /// <summary>
        /// city id of the venue
        /// </summary>
        [Column("city")]
		public int CityId { get; set; }

        /// <summary>
        /// State id of the venue
        /// </summary>
        [Column("state")]
		public int StateId { get; set; }

        /// <summary>
        /// Country id of the venue
        /// </summary>
        [Column("country")]
		public int CountryId { get; set; }

		/// <summary>
		///  Description of the venue
		/// </summary>
		[Column("description")]
		public string Description { get; set; }

		/// <summary>
		/// Creation date of the venue
		/// </summary>
		[Column("created_on")]
		public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Modified date of the venue
        /// </summary>
        [Column("modified_on")]
		public DateTime ModifiedOn { get; set; }
		public City City { get; set; }
		public State State { get; set; }
		public virtual ICollection<Event> Events { get; set; }
    }
}
