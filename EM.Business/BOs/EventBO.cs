using EM.Core.Enums;
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
        public string Title { get; set; }
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public decimal BasePrice { get; set; }

        [JsonPropertyName("organizer_id")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("performer_id")]
        public int PerformerId { get; set; }

        [JsonPropertyName("venue_id")]
        public int VenueId { get; set; }


        public StatusEnum Status { get; set; }

        [JsonPropertyName("created_on")]
        public DateTime CreatedOn { get; set; }

        [JsonPropertyName("modified_on")]
        public DateTime ModifiedOn { get; set; }

        [JsonPropertyName("start_datetime")]
        public string StartDateTime { get; set; }
        [JsonPropertyName("end_datetime")]
        public string EndDateTime { get; set; }    
    }
}
