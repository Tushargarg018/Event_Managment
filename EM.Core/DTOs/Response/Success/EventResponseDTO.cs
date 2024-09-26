using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EM.Core.DTOs.Response.Success
{
    public class EventResponseDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("base_price")]
        public decimal BasePrice { get; set; }

        [JsonPropertyName("currency")]
        public string Currency {  get; set; }

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
        public DateTime StartDateTime { get; set; }

        [JsonPropertyName("end_date_time")]
        public DateTime EndDateTime { get; set; }

        


    }
}
